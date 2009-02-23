﻿/*
 Copyright (C) 2009 Siarhei Novik (snovik@gmail.com)
  
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
    //! Helper class to build interpolated term structures
    /*! Interpolated term structures can use protected or private
        inheritance from this class to obtain the relevant data
        members and implement correct copy behavior.
    */
    public interface InterpolatedCurve : ICloneable {
        List<double> times_ { get; set; }
        List<double> times();

        List<Date> dates_ { get; set; }
        List<Date> dates();
        Date maxDate();

        List<double> data_ { get; set; }
        List<double> data();

        Dictionary<Date, double> nodes();

        Interpolation interpolation_ { get; set; }
        IInterpolationFactory interpolator_ { get; set; }
        void setupInterpolation();
    }
}
