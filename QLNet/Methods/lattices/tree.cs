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
    //! %Tree approximating a single-factor diffusion
    /*! Derived classes must implement the following interface:
        \code
        public:
          Real underlying(Size i, Size index) const;
          Size size(Size i) const;
          Size descendant(Size i, Size index, Size branch) const;
          Real probability(Size i, Size index, Size branch) const;
        \endcode
        and provide a public enumeration
        \code
        enum { branches = N };
        \endcode
        where N is a suitable constant (2 for binomial, 3 for trinomial...)

        \ingroup lattices
    */
    public class Tree<T> {
        private int columns_;
        public int columns() { return columns_; }

        // parameterless constructor is requried for generics
        public Tree() { }
        public Tree(int columns) {
            columns_ = columns;
        }
    }
}
