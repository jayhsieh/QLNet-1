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
    /*! This abstract class defines the interface of concrete
    local-volatility term structures which will be derived from this one.

    Volatilities are assumed to be expressed on an annual basis. */
    public class LocalVolTermStructure : VolatilityTermStructure {

        //! default constructor
        /*! \warning term structures initialized by means of this
                     constructor must manage their own reference date
                     by overriding the referenceDate() method.
        */
        // public LocalVolTermStructure(Calendar cal = Calendar(), BusinessDayConvention bdc = Following, DayCounter dc = DayCounter())
        public LocalVolTermStructure() : this(new Calendar(), BusinessDayConvention.Following, new DayCounter()) { }
        public LocalVolTermStructure(Calendar cal, BusinessDayConvention bdc, DayCounter dc)
            : base(cal, bdc, dc) {}

        //! initialize with a fixed reference date
        //public LocalVolTermStructure(Date referenceDate, Calendar cal = Calendar(),
        //                             BusinessDayConvention bdc = Following, DayCounter dc = DayCounter());
        public LocalVolTermStructure(Date referenceDate)
            : this(referenceDate, new Calendar(), BusinessDayConvention.Following, new DayCounter()) { }
        public LocalVolTermStructure(Date referenceDate, Calendar cal, BusinessDayConvention bdc, DayCounter dc)
            : base(referenceDate, cal, bdc, dc) { }

        //! calculate the reference date based on the global evaluation date
        //public LocalVolTermStructure(int settlementDays, Calendar cal, BusinessDayConvention bdc = Following, DayCounter dc = DayCounter());
        public LocalVolTermStructure(int settlementDays, Calendar cal)
            : this(settlementDays, cal, BusinessDayConvention.Following, new DayCounter()) { }
        public LocalVolTermStructure(int settlementDays, Calendar cal, BusinessDayConvention bdc, DayCounter dc)
            : base(settlementDays, cal, bdc, dc) { }


        //! \name Local Volatility
        //@{
        //public double localVol(Date d, Real underlyingLevel, bool extrapolate = false) {
        public double localVol(Date d, double underlyingLevel, bool extrapolate) {
            checkRange(d, extrapolate);
            checkStrike(underlyingLevel, extrapolate);
            double t = timeFromReference(d);
            return localVolImpl(t, underlyingLevel);
        }
        //public double localVol(double t, double underlyingLevel, bool extrapolate = false) {
        public double localVol(double t, double underlyingLevel, bool extrapolate) {
            checkRange(t, extrapolate);
            checkStrike(underlyingLevel, extrapolate);
            return localVolImpl(t, underlyingLevel);
        }


        /*! \name Calculations

            These methods must be implemented in derived classes to perform the actual volatility calculations. When they are called,
            range check has already been performed; therefore, they must assume that extrapolation is required. */
        //! local vol calculation
        protected virtual double localVolImpl(double t, double strike) { throw new NotSupportedException(); }
    }
}
