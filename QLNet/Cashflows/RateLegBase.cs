﻿/*
 Copyright (C) 2009 Siarhei Novik (snovik@gmail.com)
 * 
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

namespace QLNet.Cashflows {
    public abstract class RateLegBase {
        protected Schedule schedule_;
        protected List<double> notionals_;
        protected DayCounter paymentDayCounter_;
        protected BusinessDayConvention paymentAdjustment_;

        public static implicit operator List<CashFlow>(RateLegBase o) { return o.value(); }
        public abstract List<CashFlow> value();


        // initializers
        public RateLegBase withNotionals(double notional) {
            notionals_ = new List<double>() { notional };
            return this;
        }
        public RateLegBase withNotionals(List<double> notionals) {
            notionals_ = notionals;
            return this;
        }
        public RateLegBase withPaymentAdjustment(BusinessDayConvention convention) {
            paymentAdjustment_ = convention;
            return this;
        }
    }

    public abstract class FloatingLegBase : RateLegBase {
        protected InterestRateIndex index_;
        protected List<int> fixingDays_;

        protected List<double> gearings_;
        protected List<double> spreads_;
        protected List<double> caps_ = new List<double>();
        protected List<double> floors_ = new List<double>();
        protected bool inArrears_;
        protected bool zeroPayments_;

        // initializers
        public FloatingLegBase withPaymentDayCounter(DayCounter dayCounter) {
            paymentDayCounter_ = dayCounter;
            return this;
        }
        public FloatingLegBase withFixingDays(int fixingDays) {
            fixingDays_ = new List<int>() { fixingDays };
            return this;
        }
        public FloatingLegBase withFixingDays(List<int> fixingDays) {
            fixingDays_ = fixingDays;
            return this;
        }
        public FloatingLegBase withGearings(double gearing) {
            gearings_ = new List<double>() { gearing };
            return this;
        }
        public FloatingLegBase withGearings(List<double> gearings) {
            gearings_ = gearings;
            return this;
        }
        public FloatingLegBase withSpreads(double spread) {
            spreads_ = new List<double>() { spread };
            return this;
        }
        public FloatingLegBase withSpreads(List<double> spreads) {
            spreads_ = spreads;
            return this;
        }
        public FloatingLegBase withCaps(double cap) {
            caps_ = new List<double>() { cap };
            return this;
        }
        public FloatingLegBase withCaps(List<double> caps) {
            caps_ = caps;
            return this;
        }
        public FloatingLegBase withFloors(double floor) {
            floors_ = new List<double>() { floor };
            return this;
        }
        public FloatingLegBase withFloors(List<double> floors) {
            floors_ = floors;
            return this;
        }
        public FloatingLegBase inArrears() {
            return inArrears(true);
        }
        public FloatingLegBase inArrears(bool flag) {
            inArrears_ = flag;
            return this;
        }
        public FloatingLegBase withZeroPayments() {
            return withZeroPayments(true);
        }
        public FloatingLegBase withZeroPayments(bool flag) {
            zeroPayments_ = flag;
            return this;
        }
    }
}
