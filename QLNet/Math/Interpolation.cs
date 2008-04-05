/*
 Copyright (C) 2008 Siarhei Novik (snovik@gmail.com)
  
 This file is part of QLNet Project http://trac2.assembla.com/QLNet

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
    //! base class for 1-D interpolations.
    /* Classes derived from this class will provide interpolated values from two sequences of equal length,
     * representing discretized values of a variable and a function of the former, respectively. */

    // Interpolation factory
    public interface IInterpolationFactory {
        Interpolation interpolate(List<double> xBegin, List<double> yBegin);
        bool global { get; }
        int requiredPoints { get; }
    }

    public abstract class Interpolation : Extrapolator {
        protected Impl impl_;

        public bool empty() { return impl_ == null; }

        public double primitive(double x) { return primitive(x, false); }
        public double primitive(double x, bool allowExtrapolation) {
            checkRange(x,allowExtrapolation);
            return impl_.primitive(x);
        }

        public double derivative(double x) { return derivative(x, false); }
        public double derivative(double x, bool allowExtrapolation) {
            checkRange(x,allowExtrapolation);
            return impl_.derivative(x);
        }

        public double secondDerivative(double x) { return secondDerivative(x, false); }
        public double secondDerivative(double x, bool allowExtrapolation) {
            checkRange(x,allowExtrapolation);
            return impl_.derivative(x);
        }

        public double xMin() {
            return impl_.xMin();
        }
        public double xMax() {
            return impl_.xMax();
        }
        bool isInRange(double x) {
            return impl_.isInRange(x);
        }
        public void update() {
            impl_.update();
        }

        //Real operator()(Real x, bool allowExtrapolation = false) const {
        //    checkRange(x,allowExtrapolation);
        //    return impl_->value(x);
        //}
        // main method to derive an interpolated point
        public double value(double x) { return value(x, false); }
        public double value(double x, bool allowExtrapolation) {
            checkRange(x, allowExtrapolation);
            return impl_.value(x);
        }

        protected void checkRange(double x, bool extrapolate) {
            if (!(extrapolate || allowsExtrapolation() || impl_.isInRange(x)))
                throw new ArgumentException("interpolation range is [" + impl_.xMin() + ", " + impl_.xMax()
                                            + "]: extrapolation at " + x + " not allowed");
        }


        // abstract base class interface for interpolation implementations
        protected interface Impl {
            void update();
            double xMin();
            double xMax();
            List<double> xValues();
            List<double> yValues();
            bool isInRange(double d);
            double value(double d);
            double primitive(double d);
            double derivative(double d);
            double secondDerivative(double d);
        }
        public abstract class templateImpl : Impl {
            protected List<double> xBegin_;
            protected List<double> yBegin_;

            // this method should be used for initialisation
            public templateImpl(List<double> xBegin, List<double> yBegin) {
                xBegin_ = xBegin;
                yBegin_ = yBegin;
                if (xBegin_.Count < 2)
                    throw new ArgumentException("not enough points to interpolate: at least 2 required, "
                                                + (xBegin_.Count) + " provided");
            }

            public double xMin() { return xBegin_.First(); }
            public double xMax() { return xBegin_.Last(); }
            public List<double> xValues() { return xBegin_; }
            public List<double> yValues() { return yBegin_; }

            public bool isInRange(double x) {
                double x1 = xMin(), x2 = xMax();
                return (x >= x1 && x <= x2) || Comparison.close(x, x1) || Comparison.close(x, x2);
            }

            protected int locate(double x) {
                int result = xBegin_.BinarySearch(x);
                if (result < 0) result = ~result;           // value is not found and the index of the next larger item is returned

                // found or not, we take the previous item
                result -= 1;

                // finally, impose limits. we need the one before last at max or the first at min
                result = Math.Max(Math.Min(result, xBegin_.Count - 2), 0);
                return result;
            }

            public abstract double value(double d);
            public abstract void update();
            public abstract double primitive(double d);
            public abstract double derivative(double d);
            public abstract double secondDerivative(double d);
        }
    }
}
