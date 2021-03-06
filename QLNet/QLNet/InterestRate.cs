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
using System.Text;
using QLNet;
using QLNet.Time;

namespace QLNet {
    // Concrete interest rate class
    /* This class encapsulate the interest rate compounding algebra.
       It manages day-counting conventions, compounding conventions, conversion between different conventions,
     * discount/compound factor calculations, and implied/equivalent rate calculations.  */
    public class InterestRate {
        private double? r_;
        public double rate() { return r_.Value; }

        private DayCounter dc_;
        private Compounding comp_;
        private bool freqMakesSense_;
        private double freq_;      // defined as double to avoid casting

        public double value() { return rate(); }        // operator redefinition
        public DayCounter dayCounter() { return dc_; }
        public Compounding compounding() { return comp_; }
        public Frequency frequency() { return freqMakesSense_ ? (Frequency)freq_ : Frequency.NoFrequency; }

        //! Default constructor returning a null interest rate.
        public InterestRate() { r_ = null; }
        //! Standard constructor
        public InterestRate(double? r, DayCounter dc, Compounding comp) :
            this(r, dc, comp, Frequency.Annual) { }
        public InterestRate(double? r, DayCounter dc, Compounding comp, Frequency freq) {
            r_ = r;
            dc_ = dc;
            comp_ = comp;

            if (comp_ == Compounding.Compounded || comp_ == Compounding.SimpleThenCompounded)
            {
                freqMakesSense_ = true;
                if (!(freq != Frequency.Once && freq != Frequency.NoFrequency))
                    throw new ArgumentException("Frequency not allowed for this interest rate");
                freq_ = (double)freq;
            }
        }

        //! discount/compound factor calculations
        //! discount factor implied by the rate compounded at time t.
        /*! \warning Time must be measured using InterestRate's own day counter. */
        public double discountFactor(double t) { return 1 / compoundFactor(t); }

        //! discount factor implied by the rate compounded between two dates
		public double discountFactor(Date d1, Date d2) { return discountFactor(d1, d2, null, null); }
		public double discountFactor(Date d1, Date d2, Date refStart) { return discountFactor(d1, d2, refStart, null); }
        public double discountFactor(Date d1, Date d2, Date refStart, Date refEnd) {
            double t = dc_.yearFraction(d1, d2, refStart, refEnd);
            return discountFactor(t);
        }

        // compound factor implied by the rate compounded at time t.
        /* returns the compound (a.k.a capitalization) factor implied by the rate compounded at time t.
        \warning Time must be measured using InterestRate's own day counter.*/
        public double compoundFactor(double t) {
            if (t < 0) throw new ArgumentException("negative time not allowed");
            if (!r_.HasValue) throw new ArgumentException("null interest rate");
            switch (comp_) {
                case Compounding.Simple:
                    return 1 + r_.Value * t;
                case Compounding.Compounded:
                    return System.Math.Pow(1.0 + r_.Value / freq_, freq_ * t);
                case Compounding.Continuous:
                    return System.Math.Exp(r_.Value * t);
                case Compounding.SimpleThenCompounded:
                    if (t <= 1.0 / freq_) return 1 + r_.Value * t;
                    else return System.Math.Pow(1 + r_.Value / freq_, freq_ * t);
                default:
                    throw new ArgumentException("unknown compounding convention");
            }
        }

        //! compound factor implied by the rate compounded between two dates
        //! returns the compound (a.k.a capitalization) factor implied by the rate compounded between two dates.
		public double compoundFactor(Date d1, Date d2) { return compoundFactor(d1, d2, null, null); }
		public double compoundFactor(Date d1, Date d2, Date refStart) { return compoundFactor(d1, d2, refStart, null); }
        public double compoundFactor(Date d1, Date d2, Date refStart, Date refEnd) {
            double t = dc_.yearFraction(d1, d2, refStart, refEnd);
            return compoundFactor(t);
        }

        //! implied rate calculations
        //! implied interest rate for a given compound factor at a given time.
        /*! The resulting InterestRate has the day-counter provided as input. Time must be measured using the day-counter provided as input.  */
        public static InterestRate impliedRate(double compound, double t, DayCounter resultDC, Compounding comp) {
            return impliedRate(compound, t, resultDC, comp, Frequency.Annual);
        }
        public static InterestRate impliedRate(double compound, double t, DayCounter resultDC, Compounding comp, Frequency freq) {
            if (!(compound > 0.0)) throw new ArgumentException("positive compound factor required");
            if (!(t > 0.0)) throw new ArgumentException("positive time required");

            double r;
            switch (comp) {
                case Compounding.Simple:
                    r = (compound - 1.0) / t;
                    break;
                case Compounding.Compounded:
                    r = (Math.Pow(compound, 1.0/((double)freq*t))-1.0)*(double)freq;
                    break;
                case Compounding.Continuous:
                    r = System.Math.Log(compound) / t;
                    break;
                case Compounding.SimpleThenCompounded:
                    if (t <= 1.0 / (double)freq)
                        r = (compound - 1.0) / t;
                    else
                        r = (Math.Pow(compound, 1.0 / ((double)freq * t)) - 1.0) * (double)freq;
                    break;
                default:
                    throw new ArgumentException("unknown compounding convention (" + comp + ")");
            }
            return new InterestRate(r, resultDC, comp, freq);
        }

        //! implied rate for a given compound factor between two dates.
        /*! The resulting rate is calculated taking the required day-counting rule into account. */
        public static InterestRate impliedRate(double compound, Date d1, Date d2, DayCounter resultDC, Compounding comp) {
            return impliedRate(compound, d1, d2, resultDC, comp, Frequency.Annual);
        }
        public static InterestRate impliedRate(double compound, Date d1, Date d2,
                                        DayCounter resultDC, Compounding comp, Frequency freq) {
            if (!(d2 > d1))
                throw new ArgumentException("d1 (" + d1 + ") " + "later than or equal to d2 (" + d2 + ")");
            double t = resultDC.yearFraction(d1, d2);
            return impliedRate(compound, t, resultDC, comp, freq);
        }

        // equivalent rate calculations
        // equivalent interest rate for a compounding period t.
        /* The resulting InterestRate shares the same implicit
         * day-counting rule of the original InterestRate instance.
           Time must be measured using the InterestRate's own day counter. */
        public InterestRate equivalentRate(double t, Compounding comp) {
            return equivalentRate(t, comp, Frequency.Annual);
        }
        public InterestRate equivalentRate(double t, Compounding comp, Frequency freq) {
            return impliedRate(compoundFactor(t), t, dc_, comp, freq);
        }

        //! equivalent rate for a compounding period between two dates
        /*! The resulting rate is calculated taking the required day-counting rule into account. */
        public InterestRate equivalentRate(Date d1, Date d2, DayCounter resultDC, Compounding comp) {
            return equivalentRate(d1, d2, resultDC, comp, Frequency.Annual);
        }
        public InterestRate equivalentRate(Date d1, Date d2, DayCounter resultDC, Compounding comp, Frequency freq) {
            if (!(d2 > d1)) throw new ArgumentException("d1 (" + d1 + ") " +
                                                        "later than or equal to d2 (" + d2 + ")");
            double t1 = dc_.yearFraction(d1, d2);
            double t2 = resultDC.yearFraction(d1, d2);
            return impliedRate(compoundFactor(t1), t2, resultDC, comp, freq);
        }


		public override string ToString() {
			string result = "";
			if (r_ == null)
				return "null interest rate";

			result += string.Format("{0:0.00%}", rate()) + " " + dayCounter().Name + " ";
			switch (compounding()) {
				case Compounding.Simple:
					result += "simple compounding";
					break;
				case Compounding.Compounded:
					switch (frequency()) {
						case Frequency.NoFrequency:
						case Frequency.Once:
							throw new ApplicationException(frequency() + " frequency not allowed for this interest rate");
						default:
							result += frequency() +" compounding";
							break;
					}
					break;
				case Compounding.Continuous:
					result += "continuous compounding";
					break;
			  case Compounding.SimpleThenCompounded:
					switch (frequency()) {
						case Frequency.NoFrequency:
						case Frequency.Once:
							throw new ApplicationException(frequency() + " frequency not allowed for this interest rate");
						default:
							result += "simple compounding up to "
								+ (int)(12/(int)frequency()) + " months, then "
								+ frequency() + " compounding";
							break;
					}
					break;
			  default:
				throw new ApplicationException("unknown compounding convention (" + compounding() + ")");
			}
			return result;
		}
    }
}
