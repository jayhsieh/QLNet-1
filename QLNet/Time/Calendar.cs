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
using System.Text;
using System.Runtime.InteropServices;

namespace QLNet
{
    //! %calendar class
    /*! This class provides methods for determining whether a date is a
         business day or a holiday for a given market, and for
         incrementing/decrementing a date of a given number of business days.

        A calendar should be defined for specific exchange holiday schedule
        or for general country holiday schedule. Legacy city holiday schedule
        calendars will be moved to the exchange/country convention.   */
    public class Calendar
    {
        protected Calendar calendar_;
        public Calendar calendar
        {
            get { return calendar_; }
            set { calendar_ = value; }
        }

        // constructors
        /*! The default constructor returns a calendar with a null implementation, which is therefore unusable except as a
            placeholder. */
        public Calendar() { }
        public Calendar(Calendar c) { calendar_ = c; }

        // wrappers for interface
        public virtual string name() { return calendar.name(); }
        public virtual bool isBusinessDay(Date d) { return calendar.isBusinessDay(d); }
        public virtual bool isWeekend(DayOfWeek w) { return calendar.isWeekend(w); }

        // other functions
        public bool empty() { return calendar == null; }				//!  Returns whether or not the calendar is initialized
        public bool isHoliday(Date d) { return !isBusinessDay(d); }
        public bool isEndOfMonth(Date d) { return (d.Month != adjust(d + 1).Month); }
        public Date endOfMonth(Date d) { return adjust(d.endOfMonth(d), BusinessDayConvention.Preceding); }

        // Adjusts a non-business day to the appropriate near business day  with respect to the given convention. 
        public Date adjust(Date d) { return adjust(d, BusinessDayConvention.Following); }
        public Date adjust(Date d, BusinessDayConvention c)
        {
            if (d == null) throw new ArgumentException("null date");
            if (c == BusinessDayConvention.Unadjusted) return d;

            Date d1 = d;
            if (c == BusinessDayConvention.Following || c == BusinessDayConvention.ModifiedFollowing)
            {
                while (isHoliday(d1)) d1++;
                if (c == BusinessDayConvention.ModifiedFollowing)
                {
                    if (d1.Month != d.Month)
                        return adjust(d, BusinessDayConvention.Preceding);
                }
            }
            else if (c == BusinessDayConvention.Preceding || c == BusinessDayConvention.ModifiedPreceding)
            {
                while (isHoliday(d1))
                    d1--;
                if (c == BusinessDayConvention.ModifiedPreceding && d1.Month != d.Month)
                    return adjust(d, BusinessDayConvention.Following);
            }
            else throw Error.UnknownBusinessDayConvention(c);
            return d1;
        }

        // Advances the given date of the given number of business days and returns the result. The input date is not modified.
        public Date advance(Date d, int n, TimeUnit unit) { return advance(d, n, unit, BusinessDayConvention.Following, false); }
        public Date advance(Date d, int n, TimeUnit unit, BusinessDayConvention c) { return advance(d, n, unit, c, false); }
        public Date advance(Date d, int n, TimeUnit unit, BusinessDayConvention c, bool endOfMonth)
        {
            if (d == null) throw new ArgumentException("null date");
            if (n == 0)
                return adjust(d, c);
            else if (unit == TimeUnit.Days)
            {
                Date d1 = d;
                if (n > 0)
                {
                    while (n > 0)
                    {
                        d1++;
                        while (isHoliday(d1))
                            d1++;
                        n--;
                    }
                }
                else
                {
                    while (n < 0)
                    {
                        d1--;
                        while (isHoliday(d1))
                            d1--;
                        n++;
                    }
                }
                return d1;
            }
            else if (unit == TimeUnit.Weeks)
            {
                Date d1 = d + new Period(n, unit);
                return adjust(d1, c);
            }
            else
            {
                Date d1 = d + new Period(n, unit);
                if (endOfMonth && (unit == TimeUnit.Months || unit == TimeUnit.Years) && isEndOfMonth(d))
                    return d1.endOfMonth(d1);
                return adjust(d1, c);
            }
        }
        // Advances the given date as specified by the given period and returns the result. The input date is not modified.
        public Date advance(Date d, Period p) { return advance(d, p, BusinessDayConvention.Following, false); }
        public Date advance(Date d, Period p, BusinessDayConvention c) { return advance(d, p, c, false); }
        public Date advance(Date d, Period p, BusinessDayConvention c, bool endOfMonth)
        {
            return advance(d, p.length(), p.units(), c, endOfMonth);
        }

        // Calculates the number of business days between two given dates and returns the result.
        public int businessDaysBetween(Date from, Date to) { return businessDaysBetween(from, to, true, false); }
        public int businessDaysBetween(Date from, Date to, bool includeFirst) { return businessDaysBetween(from, to, includeFirst, false); }
        public int businessDaysBetween(Date from, Date to, bool includeFirst, bool includeLast)
        {
            int wd = 0;
            if (from != to)
            {
                if (from < to)
                {
                    // the last one is treated separately to avoid incrementing Date::maxDate()
                    for (Date d = from; d < to; ++d)
                    {
                        if (isBusinessDay(d))
                            ++wd;
                    }
                    if (isBusinessDay(to))
                        ++wd;
                }
                else if (from > to)
                {
                    for (Date d = to; d < from; ++d)
                    {
                        if (isBusinessDay(d))
                            ++wd;
                    }
                    if (isBusinessDay(from))
                        ++wd;
                }

                if (isBusinessDay(from) && !includeFirst)
                    wd--;
                if (isBusinessDay(to) && !includeLast)
                    wd--;

                if (from > to)
                    wd = -wd;
            }
            return wd;
        }

        public class WesternImpl : Calendar
        {		// Western calendars
            public WesternImpl() { }
            public WesternImpl(Calendar c) : base(c) { }

            public override bool isWeekend(DayOfWeek w) { return w == DayOfWeek.Saturday || w == DayOfWeek.Sunday; }
            public int easterMonday(int y)
            {
                int[] EasterMonday = {
		                  98,  90, 103,  95, 114, 106,  91, 111, 102,   // 1901-1909
		             87, 107,  99,  83, 103,  95, 115,  99,  91, 111,   // 1910-1919
		             96,  87, 107,  92, 112, 103,  95, 108, 100,  91,   // 1920-1929
		            111,  96,  88, 107,  92, 112, 104,  88, 108, 100,   // 1930-1939
		             85, 104,  96, 116, 101,  92, 112,  97,  89, 108,   // 1940-1949
		            100,  85, 105,  96, 109, 101,  93, 112,  97,  89,   // 1950-1959
		            109,  93, 113, 105,  90, 109, 101,  86, 106,  97,   // 1960-1969
		             89, 102,  94, 113, 105,  90, 110, 101,  86, 106,   // 1970-1979
		             98, 110, 102,  94, 114,  98,  90, 110,  95,  86,   // 1980-1989
		            106,  91, 111, 102,  94, 107,  99,  90, 103,  95,   // 1990-1999
		            115, 106,  91, 111, 103,  87, 107,  99,  84, 103,   // 2000-2009
		             95, 115, 100,  91, 111,  96,  88, 107,  92, 112,   // 2010-2019
		            104,  95, 108, 100,  92, 111,  96,  88, 108,  92,   // 2020-2029
		            112, 104,  89, 108, 100,  85, 105,  96, 116, 101,   // 2030-2039
		             93, 112,  97,  89, 109, 100,  85, 105,  97, 109,   // 2040-2049
		            101,  93, 113,  97,  89, 109,  94, 113, 105,  90,   // 2050-2059
		            110, 101,  86, 106,  98,  89, 102,  94, 114, 105,   // 2060-2069
		             90, 110, 102,  86, 106,  98, 111, 102,  94, 114,   // 2070-2079
		             99,  90, 110,  95,  87, 106,  91, 111, 103,  94,   // 2080-2089
		            107,  99,  91, 103,  95, 115, 107,  91, 111, 103,   // 2090-2099
		             88, 108, 100,  85, 105,  96, 109, 101,  93, 112,   // 2100-2109
		             97,  89, 109,  93, 113, 105,  90, 109, 101,  86,   // 2110-2119
		            106,  97,  89, 102,  94, 113, 105,  90, 110, 101,   // 2120-2129
		             86, 106,  98, 110, 102,  94, 114,  98,  90, 110,   // 2130-2139
		             95,  86, 106,  91, 111, 102,  94, 107,  99,  90,   // 2140-2149
		            103,  95, 115, 106,  91, 111, 103,  87, 107,  99,   // 2150-2159
		             84, 103,  95, 115, 100,  91, 111,  96,  88, 107,   // 2160-2169
		             92, 112, 104,  95, 108, 100,  92, 111,  96,  88,   // 2170-2179
		            108,  92, 112, 104,  89, 108, 100,  85, 105,  96,   // 2180-2189
		            116, 101,  93, 112,  97,  89, 109, 100,  85, 105    // 2190-2199
		        };
                return EasterMonday[y - 1901];
            }
        }
        public class OrthodoxImpl : Calendar
        {		// Orthodox calendars
            public OrthodoxImpl() { }
            public OrthodoxImpl(Calendar c) : base(c) { }

            public override bool isWeekend(DayOfWeek w) { return w == DayOfWeek.Saturday || w == DayOfWeek.Sunday; }
            public int easterMonday(int y)
            {
                int[] EasterMonday = {
		                 105, 118, 110, 102, 121, 106, 126, 118, 102,   // 1901-1909
		            122, 114,  99, 118, 110,  95, 115, 106, 126, 111,   // 1910-1919
		            103, 122, 107,  99, 119, 110, 123, 115, 107, 126,   // 1920-1929
		            111, 103, 123, 107,  99, 119, 104, 123, 115, 100,   // 1930-1939
		            120, 111,  96, 116, 108, 127, 112, 104, 124, 115,   // 1940-1949
		            100, 120, 112,  96, 116, 108, 128, 112, 104, 124,   // 1950-1959
		            109, 100, 120, 105, 125, 116, 101, 121, 113, 104,   // 1960-1969
		            117, 109, 101, 120, 105, 125, 117, 101, 121, 113,   // 1970-1979
		             98, 117, 109, 129, 114, 105, 125, 110, 102, 121,   // 1980-1989
		            106,  98, 118, 109, 122, 114, 106, 118, 110, 102,   // 1990-1999
		            122, 106, 126, 118, 103, 122, 114,  99, 119, 110,   // 2000-2009
		             95, 115, 107, 126, 111, 103, 123, 107,  99, 119,   // 2010-2019
		            111, 123, 115, 107, 127, 111, 103, 123, 108,  99,   // 2020-2029
		            119, 104, 124, 115, 100, 120, 112,  96, 116, 108,   // 2030-2039
		            128, 112, 104, 124, 116, 100, 120, 112,  97, 116,   // 2040-2049
		            108, 128, 113, 104, 124, 109, 101, 120, 105, 125,   // 2050-2059
		            117, 101, 121, 113, 105, 117, 109, 101, 121, 105,   // 2060-2069
		            125, 110, 102, 121, 113,  98, 118, 109, 129, 114,   // 2070-2079
		            106, 125, 110, 102, 122, 106,  98, 118, 110, 122,   // 2080-2089
		            114,  99, 119, 110, 102, 115, 107, 126, 118, 103,   // 2090-2099
		            123, 115, 100, 120, 112,  96, 116, 108, 128, 112,   // 2100-2109
		            104, 124, 109, 100, 120, 105, 125, 116, 108, 121,   // 2110-2119
		            113, 104, 124, 109, 101, 120, 105, 125, 117, 101,   // 2120-2129
		            121, 113,  98, 117, 109, 129, 114, 105, 125, 110,   // 2130-2139
		            102, 121, 113,  98, 118, 109, 129, 114, 106, 125,   // 2140-2149
		            110, 102, 122, 106, 126, 118, 103, 122, 114,  99,   // 2150-2159
		            119, 110, 102, 115, 107, 126, 111, 103, 123, 114,   // 2160-2169
		             99, 119, 111, 130, 115, 107, 127, 111, 103, 123,   // 2170-2179
		            108,  99, 119, 104, 124, 115, 100, 120, 112, 103,   // 2180-2189
		            116, 108, 128, 119, 104, 124, 116, 100, 120, 112    // 2190-2199
		        };
                return EasterMonday[y - 1901];
            }
        }
    }
}
