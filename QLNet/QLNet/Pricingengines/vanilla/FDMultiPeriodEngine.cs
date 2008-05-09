﻿/*
 Copyright (C) 2008 Siarhei Novik (snovik@gmail.com)
  
 This file is part of QLNet Project http://www.qlnet.org

 QLNet is free software: you can redistribute it and/or modify it
 under the terms of the QLNet license.  You should have received a
 copy of the license along with this program; if not, license is  
 available online at <http://trac2.assembla.com/QLNet/wiki/License>.
  
 QLNet is a based on QuantLib, a free-software/open-source library
 for financial quantitative analysts and developers - http://quantlib.org/
 The QuantLib license is available online at http://quantlib.org/license.shtml.
 
 This program is distributed in the hope that it will be useful, but WITHOUT
 ANY WARRANTY; without even the implied warranty of MERCHANTABILITY or FITNESS
 FOR A PARTICULAR PURPOSE.  See the license for more details.
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace QLNet {
    public abstract class FDMultiPeriodEngine : FDVanillaEngine {
        protected List<Event> events_;
        protected List<double> stoppingTimes_;
        protected int timeStepPerPeriod_;
        protected SampledCurve prices_;

        protected IStepCondition<Vector> stepCondition_;
        protected FiniteDifferenceModel<CrankNicolson<TridiagonalOperator>> model_;

        
        //protected FDMultiPeriodEngine(GeneralizedBlackScholesProcess process,
        //     int gridPoints = 100, int timeSteps = 100, bool timeDependent = false)    
        protected FDMultiPeriodEngine(GeneralizedBlackScholesProcess process, int gridPoints, int timeSteps, bool timeDependent)    
            : base(process, gridPoints, timeSteps, timeDependent) {
            timeStepPerPeriod_ = timeSteps;
        }

        protected virtual void setupArguments(IPricingEngineArguments args, List<Event> schedule) {
            base.setupArguments(args);
            events_ = schedule;
            stoppingTimes_.Clear();
            int n = schedule.Count;
            stoppingTimes_ = new List<double>(n);
            for (int i=0; i<n; ++i)
                stoppingTimes_.Add(process_.time(events_[i].date()));
        }

        protected override void setupArguments(IPricingEngineArguments a) {
            base.setupArguments(a);
            OneAssetOption.Arguments args = a as OneAssetOption.Arguments;
            if (args == null) throw new ApplicationException("incorrect argument type");
            events_.Clear();

            int n = args.exercise.dates().Count;
            stoppingTimes_ = new List<double>(n);
            for (int i=0; i<n; ++i)
                stoppingTimes_[i] = process_.time(args.exercise.date(i));
        }

        protected virtual void calculate(IPricingEngineResults r) {
            OneAssetOption.Results results = r as OneAssetOption.Results;
            if (results == null) throw new ApplicationException("incorrect results type");

            double beginDate, endDate;
            int dateNumber = stoppingTimes_.Count;
            bool lastDateIsResTime = false;
            int firstIndex = -1;
            int lastIndex = dateNumber - 1;
            bool firstDateIsZero = false;
            double firstNonZeroDate = getResidualTime();

            double dateTolerance = 1e-6;
            int j;

            if (dateNumber > 0) {
                if (!(getDividendTime(0) >= 0))
                    throw new ApplicationException("first date (" + getDividendTime(0) + ") cannot be negative");
                if(getDividendTime(0) < getResidualTime() * dateTolerance ){
                    firstDateIsZero = true;
                    firstIndex = 0;
                    if(dateNumber >= 2)
                        firstNonZeroDate = getDividendTime(1);
                }

                if (Math.Abs(getDividendTime(lastIndex) - getResidualTime()) < dateTolerance) {
                    lastDateIsResTime = true;
                    lastIndex = dateNumber - 2;
                }

                if (!firstDateIsZero)
                    firstNonZeroDate = getDividendTime(0);

                if (dateNumber >= 2) {
                    for (j = 1; j < dateNumber; j++)
                        if (!(getDividendTime(j-1) < getDividendTime(j)))
                            throw new ApplicationException("dates must be in increasing order: "
                                   + getDividendTime(j-1) + " is not strictly smaller than " + getDividendTime(j));
                }
            }

            double dt = getResidualTime()/(timeStepPerPeriod_*(dateNumber+1));

            // Ensure that dt is always smaller than the first non-zero date
            if (firstNonZeroDate <= dt)
                dt = firstNonZeroDate/2.0;

            setGridLimits();
            initializeInitialCondition();
            initializeOperator();
            initializeBoundaryConditions();
            initializeModel();
            initializeStepCondition();

            prices_ = intrinsicValues_;
            if(lastDateIsResTime)
                executeIntermediateStep(dateNumber - 1);

            j = lastIndex;
            object temp;
            do {
                if (j == dateNumber - 1)
                    beginDate = getResidualTime();
                else
                    beginDate = getDividendTime(j+1);

                if (j >= 0)
                    endDate = getDividendTime(j);
                else
                    endDate = dt;

                temp = prices_.values();
                model_.rollback(ref temp, beginDate, endDate, timeStepPerPeriod_, stepCondition_);
                prices_.setValues((Vector)temp);
                
                if (j >= 0)
                    executeIntermediateStep(j);
            } while (--j >= firstIndex);

            temp = prices_.values();
            model_.rollback(ref temp, dt, 0, 1, stepCondition_);
            prices_.setValues((Vector)temp);

            if(firstDateIsZero)
                executeIntermediateStep(0);

            results.value = prices_.valueAtCenter();
            results.delta = prices_.firstDerivativeAtCenter();
            results.gamma = prices_.secondDerivativeAtCenter();
            results.additionalResults.Add("priceCurve", prices_);
        }

        protected abstract void executeIntermediateStep(int step);

        protected virtual void initializeStepCondition() {
            stepCondition_ = new NullCondition<Vector>();
        }

        protected virtual void initializeModel() {
            model_ = new FiniteDifferenceModel<CrankNicolson<TridiagonalOperator>>(finiteDifferenceOperator_,BCs_);
        }

        protected double getDividendTime(int i)  {
            return stoppingTimes_[i];
        }
    }
}
