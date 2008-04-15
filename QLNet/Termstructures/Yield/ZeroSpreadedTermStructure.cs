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

namespace QLNet {
    //! Term structure with an added spread on the zero yield rate
    /*! \note This term structure will remain linked to the original structure, i.e., any changes in the latter will be
              reflected in this structure as well.

        \ingroup yieldtermstructures

        \test
        - the correctness of the returned values is tested by checking them against numerical calculations.
        - observability against changes in the underlying term structure and in the added spread is checked.
    */
    public class ZeroSpreadedTermStructure : ZeroYieldStructure {
        private Handle<YieldTermStructure> originalCurve_;
        private Quote spread_;
        private Compounding comp_;
        private Frequency freq_;
        private DayCounter dc_;

        public ZeroSpreadedTermStructure(Handle<YieldTermStructure> h, Quote spread)
            : this(h, spread, Compounding.Continuous, Frequency.NoFrequency, new DayCounter()) { }
        public ZeroSpreadedTermStructure(Handle<YieldTermStructure> h, Quote spread, Compounding comp, Frequency freq, DayCounter dc) {
            originalCurve_ = h;
            spread_ = spread;
            comp_ = comp;
            freq_ = freq;
            dc_ = dc;
            //QL_REQUIRE(h->dayCounter()==dc_,
            //           "spread daycounter (" << dc_ <<
            //           ") must be the same of the curve to be spreaded (" <<
            //           originalCurve_->dayCounter() <<
            //           ")");
            originalCurve_.registerWith(update);
            spread_.registerWith(update);
        }

        public override DayCounter dayCounter() { return originalCurve_.link.dayCounter(); }
        public override Calendar calendar() { return originalCurve_.link.calendar(); }
        public override int settlementDays() { return originalCurve_.link.settlementDays(); }
        public override Date referenceDate() { return originalCurve_.link.referenceDate(); }
        public override Date maxDate() { return originalCurve_.link.maxDate(); }
        public override double maxTime() { return originalCurve_.link.maxTime(); }

        protected override double zeroYieldImpl(double t) {
            // to be fixed: user-defined daycounter should be used
            InterestRate zeroRate = originalCurve_.link.zeroRate(t, comp_, freq_, true);
            InterestRate spreadedRate = new InterestRate(zeroRate.value() + spread_.value(), zeroRate.dayCounter(),
                                                         zeroRate.compounding(), zeroRate.frequency());
            return spreadedRate.equivalentRate(t, Compounding.Continuous, Frequency.NoFrequency).value();
        }

        public double forwardImpl(double t) {
            return originalCurve_.link.forwardRate(t, t, comp_, freq_, true).value() + spread_.value();
        }

    }
}
