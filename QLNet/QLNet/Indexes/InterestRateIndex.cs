/*
 Copyright (C) 2008 Siarhei Novik (snovik@gmail.com)
 Copyright (C) 2008 Toyin Akin (toyin_akin@hotmail.com)

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
using QLNet.Currencies;
using QLNet.Time;

namespace QLNet
{
	//! base class for interest rate indexes
	/*! \todo add methods returning InterestRate */
	public abstract class InterestRateIndex : Index, IObserver
	{
		#region properties
		protected string familyName_;
		public string familyName() { return familyName_; }

		protected Period tenor_;
		public Period tenor() { return tenor_; }

		protected int fixingDays_;
		public int fixingDays() { return fixingDays_; }

		protected Calendar fixingCalendar_;

		protected Currency currency_;
		public Currency currency() { return currency_; }

		protected DayCounter dayCounter_;
		public DayCounter dayCounter() { return dayCounter_; }
		#endregion

		protected InterestRateIndex()
		{
		}

		protected InterestRateIndex(string familyName, Period tenor, int fixingDays, Currency currency, Calendar fixingCalendar, DayCounter dayCounter)
		{
			familyName_ = familyName;
			tenor_ = tenor;
			fixingDays_ = fixingDays;
			currency_ = currency;
			fixingCalendar_ = fixingCalendar;
			dayCounter_ = dayCounter;

			tenor_.Normalize();

			Settings.registerWith(update);
			// recheck
			IndexManager.instance().notifier(name()).registerWith(update);
		}

		public void update()
		{
			notifyObservers();
		}

		public override string name()
		{
			string res = familyName_;
			if (tenor_ == new Period(1, TimeUnit.Days))
			{
				if (fixingDays_ == 0)
					res += "ON";
				else if (fixingDays_ == 1)
					res += "TN";
				else if (fixingDays_ == 2)
					res += "SN";
				else
					res += tenor_.ToShortString();
			}
			else
				res += tenor_.ToShortString();
			res = res + " " + dayCounter_.Name;
			return res;
		}

		public override Calendar fixingCalendar()
		{
			return fixingCalendar_;
		}

		public override bool isValidFixingDate(Date fixingDate)
		{
			return fixingCalendar_.isBusinessDay(fixingDate);
		}

		public override double fixing(Date fixingDate, bool forecastTodaysFixing)
		{
			if (!isValidFixingDate(fixingDate))
				throw new ArgumentException("Fixing date " + fixingDate + " is not valid");

			TimeSeries<double> fixings = IndexManager.instance().getHistory(name()).value();
			if (fixings.ContainsKey(fixingDate))
			{
				return fixings[fixingDate];
			}
			
			Date today = Settings.evaluationDate();
			if (fixingDate < today
			    || (fixingDate == today && !forecastTodaysFixing && Settings.enforcesTodaysHistoricFixings))
			{
				// must have been fixed
				if (IndexManager.MissingPastFixingCallBack == null)
				{
					throw new ArgumentException("Missing " + name() + " fixing for " + fixingDate);
				}
				
				// try to load missing fixing from external source
				double fixing = IndexManager.MissingPastFixingCallBack(this, fixingDate);
				// add to history
				addFixing(fixingDate, fixing);
				return fixing;
			}

			if ((fixingDate == today) && !forecastTodaysFixing)
			{
				// might have been fixed but forecast since it does not exist
				// so fall through and forecast
			}

			// forecast
			return forecastFixing(fixingDate);
		}

		public virtual Date valueDate(Date fixingDate)
		{
			if (!isValidFixingDate(fixingDate))
			{
				throw new ArgumentException("fixing date " + fixingDate + " is not valid");
			}

			return fixingCalendar().advance(fixingDate, fixingDays_, TimeUnit.Days);
		}

		public abstract Date maturityDate(Date valueDate);

		public Date fixingDate(Date valueDate)
		{
			Date fixingDate = fixingCalendar().advance(valueDate, -1 * fixingDays_, TimeUnit.Days);

			if (!isValidFixingDate(fixingDate))
			{
				throw new ArgumentException("fixing date " + fixingDate + " is not valid");
			}

			return fixingDate;
		}

		protected abstract double forecastFixing(Date fixingDate);
	}
}
