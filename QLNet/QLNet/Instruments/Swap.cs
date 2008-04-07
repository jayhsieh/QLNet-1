/*
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

namespace QLNet
{
    // Interest rate swap
    // The cash flows belonging to the first leg are paid; the ones belonging to the second leg are received.
    public class Swap : Instrument
    {
        protected Array<List<CashFlow>> legs_;
        protected Array<double> payer_;
        protected Array<double> legNPV_;
        protected Array<double> legBPS_;

        public Arguments arguments;
        public Results results;
        public Engine engine;

        #region ctors
        // The cash flows belonging to the first leg are paid; the ones belonging to the second leg are received.
        public Swap(List<CashFlow> firstLeg, List<CashFlow> secondLeg)
        {
            legs_ = new Array<List<CashFlow>>(2);
            legs_[0] = firstLeg;
            legs_[1] = secondLeg;
            payer_ = new Array<double>(2);
            payer_[0] = -1.0;
            payer_[1] = 1.0;
            legNPV_ = new Array<double>(2);
            legBPS_ = new Array<double>(2);

            for (int i = 0; i < legs_.Count; i++)
                for (int j = 0; j < legs_[i].Count; j++)
                    legs_[i][j].registerWith(update);
        }

        // Multi leg constructor.
        public Swap(List<List<CashFlow>> legs, List<bool> payer)
        {
            legs_ = (Array<List<CashFlow>>)legs;
            payer_ = new Array<double>(legs.Count);
            legNPV_ = new Array<double>(legs.Count);
            legBPS_ = new Array<double>(legs.Count);
            if (payer.Count != legs_.Count) throw new ArgumentException(
                         "size mismatch between payer (" + payer.Count + ") and legs (" + legs_.Count + ")");
            for (int i = 0; i < legs_.Count; ++i)
            {
                if (payer[i]) payer_[i] = -1;
                for (int j = 0; j < legs_[i].Count; j++)
                    legs_[i][j].registerWith(update);
            }
        }

        public Swap(int legs)
        {
            legs_ = new Array<List<CashFlow>>(legs);
            payer_ = new Array<double>(legs);
            legNPV_ = new Array<double>(legs);
            legBPS_ = new Array<double>(legs);
        }
        #endregion

        ///////////////////////////////////////////////////////////////////
        // Instrument interface
        public override bool isExpired()
        {
            Date today = Settings.evaluationDate();
            foreach (var leg in legs_)
            {
                foreach (var cf in leg)
                    if (!cf.hasOccurred(today)) return false;
            }
            return true;
        }

        protected override void setupExpired()
        {
            base.setupExpired();
            legBPS_ = new Array<double>(legBPS_.Count);
            legNPV_ = new Array<double>(legNPV_.Count);
        }

        public override void setupArguments(PricingEngine.Arguments args)
        {
            Swap.Arguments arguments = args as Swap.Arguments;
            if (arguments == null) throw new ArgumentException("wrong argument type");

            arguments.legs = legs_;
            arguments.payer = payer_;
        }

        public override void fetchResults(PricingEngine.Results r)
        {
            base.fetchResults(r);

            Swap.Results results = r as Swap.Results;
            if (results == null) throw new ArgumentException("wrong result type");

            if (results.legNPV.Count != 0)
            {
                if (results.legNPV.Count != legNPV_.Count)
                    throw new ArgumentException("wrong number of leg NPV returned");
                legNPV_ = results.legNPV;
            }
            else
            {
                legNPV_ = new Array<double>(legNPV_.Count);
            }

            if (results.legBPS.Count != 0)
            {
                if (results.legBPS.Count != legBPS_.Count)
                    throw new ArgumentException("wrong number of leg BPS returned");
                legBPS_ = results.legBPS;
            }
            else
            {
                legBPS_ = new Array<double>(legBPS_.Count);
            }
        }


        ///////////////////////////////////////////////////////////////////
        // CashFlow interface
        public Date startDate()
        {
            if (legs_.Count == 0) throw new ArgumentException("no legs given");
            Date d = CashFlows.startDate(legs_[0]);
            foreach (List<CashFlow> cf in legs_)
                d = Date.Min(d, CashFlows.startDate(cf));
            return d;
        }

        public Date maturityDate()
        {
            if (legs_.Count == 0) throw new ArgumentException("no legs given");
            Date d = CashFlows.maturityDate(legs_[0]);
            foreach (List<CashFlow> cf in legs_)
                d = Date.Max(d, CashFlows.maturityDate(cf));
            return d;
        }


        ///////////////////////////////////////////////////////////////////
        // additional interface to Swap
        public double legBPS(int j)
        {
            if (j >= legs_.Count) throw new ArgumentException("leg# " + j + " doesn't exist!");
            calculate();
            return legBPS_[j];
        }
        public double legNPV(int j)
        {
            if (j >= legs_.Count) throw new ArgumentException("leg# " + j + " doesn't exist!");
            calculate();
            return legNPV_[j];
        }
        public List<CashFlow> leg(int j)
        {
            if (j >= legs_.Count) throw new ArgumentException("leg# " + j + " doesn't exist!");
            return legs_[j];
        }

        ////////////////////////////////////////////////////////////////
        // arguments, results, pricing engine
        public class Arguments : PricingEngine.Arguments
        {
            public List<List<CashFlow>> legs;
            public List<double> payer;
            public override void Validate()
            {
                if (legs.Count != payer.Count) throw new ArgumentException("number of legs and multipliers differ");
            }
        }

        public new class Results : Instrument.Results
        {
            public Array<double> legNPV = new Array<double>();
            public Array<double> legBPS = new Array<double>();
            public override void Reset()
            {
               base.Reset();
               // clear all previous results
               legNPV.Erase();
               legBPS.Erase();
            }
        }

        public abstract class Engine : GenericEngine<Swap.Arguments, Swap.Results> { }
    }
}
