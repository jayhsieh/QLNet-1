/*
 Copyright (C) 2008 Alessandro Duci
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

namespace QLNet {
    //! Hong Kong calendars
    /*! Holidays:
        <ul>
        <li>Saturdays</li>
        <li>Sundays</li>
        <li>New Year's Day, January 1st (possibly moved to Monday)</li>
        <li>Ching Ming Festival, April 5th </li>
        <li>Good Friday</li>
        <li>Easter Monday</li>
        <li>Labor Day, May 1st</li>
        <li>SAR Establishment Day, July 1st (possibly moved to Monday)</li>
        <li>National Day, October 1st (possibly moved to Monday)</li>
        <li>Christmas, December 25th</li>
        <li>Boxing Day, December 26th (possibly moved to Monday)</li>
        </ul>

        Other holidays for which no rule is given
        (data available for 2004-2007 only:)
        <ul>
        <li>Lunar New Year</li>
        <li>Chinese New Year</li>
        <li>Buddha's birthday</li>
        <li>Tuen NG Festival</li>
        <li>Mid-autumn Festival</li>
        <li>Chung Yeung Festival</li>
        </ul>

        Data from <http://www.hkex.com.hk>

        \ingroup calendars
    */
    public class HongKong : Calendar {
        public HongKong() : base(Impl.Singleton) { }

        class Impl : Calendar.WesternImpl {
            public static readonly Impl Singleton = new Impl();
            private Impl() { }
        
            public override string name() { return "Hong Kong stock exchange"; }
            public override bool isBusinessDay(Date date) {
                DayOfWeek w = date.DayOfWeek;
                int d = date.Day, dd = date.DayOfYear;
                Month m = (Month)date.Month;
                int y = date.Year;
                int em = easterMonday(y);

                if (isWeekend(w)
                    // New Year's Day
                    || ((d == 1 || ((d == 2 || d == 3) && w == DayOfWeek.Monday))
                        && m == Month.January)
                    // Ching Ming Festival
                    || (d == 5 && m == Month.April)
                    // Good Friday
                    || (dd == em-3)
                    // Easter Monday
                    || (dd == em)
                    // Labor Day
                    || (d == 1 && m == Month.May)
                    // SAR Establishment Day
                    || ((d == 1 || ((d == 2 || d == 3) && w == DayOfWeek.Monday)) && m == Month.July)
                    // National Day
                    || ((d == 1 || ((d == 2 || d == 3) && w == DayOfWeek.Monday))
                        && m == Month.October)
                    // Christmas Day
                    || (d == 25 && m == Month.December)
                    // Boxing Day
                    || ((d == 26 || ((d == 27 || d == 28) && w == DayOfWeek.Monday))
                        && m == Month.December))
                    return false;

                if (y == 2004) {
                    if (// Lunar New Year
                        ((d==22 || d==23 || d==24) && m == Month.January)
                        // Buddha's birthday
                        || (d == 26 && m == Month.May)
                        // Tuen NG festival
                        || (d == 22 && m == Month.June)
                        // Mid-autumn festival
                        || (d == 29 && m == Month.September)
                        // Chung Yeung
                        || (d == 29 && m == Month.September))
                        return false;
                }

                if (y == 2005) {
                    if (// Lunar New Year
                        ((d==9 || d==10 || d==11) && m == Month.February)
                        // Buddha's birthday
                        || (d == 16 && m == Month.May)
                        // Tuen NG festival
                        || (d == 11 && m == Month.June)
                        // Mid-autumn festival
                        || (d == 19 && m == Month.September)
                        // Chung Yeung festival
                        || (d == 11 && m == Month.October))
                    return false;
                }

                if (y == 2006) {
                    if (// Lunar New Year
                        ((d >= 28 && d <= 31) && m == Month.January)
                        // Buddha's birthday
                        || (d == 5 && m == Month.May)
                        // Tuen NG festival
                        || (d == 31 && m == Month.May)
                        // Mid-autumn festival
                        || (d == 7 && m == Month.October)
                        // Chung Yeung festival
                        || (d == 30 && m == Month.October))
                    return false;
                }

                if (y == 2007) {
                    if (// Lunar New Year
                        ((d >= 17 && d <= 20) && m == Month.February)
                        // Buddha's birthday
                        || (d == 24 && m == Month.May)
                        // Tuen NG festival
                        || (d == 19 && m == Month.June)
                        // Mid-autumn festival
                        || (d == 26 && m == Month.September)
                        // Chung Yeung festival
                        || (d == 19 && m == Month.October))
                    return false;
                }

                if (y == 2008) {
                    if (// Lunar New Year
                        ((d >= 7 && d <= 9) && m == Month.February)
                        // Ching Ming Festival
                        || (d == 4 && m == Month.April)
                        // Buddha's birthday
                        || (d == 12 && m == Month.May)
                        // Tuen NG festival
                        || (d == 9 && m == Month.June)
                        // Mid-autumn festival
                        || (d == 15 && m == Month.September)
                        // Chung Yeung festival
                        || (d == 7 && m == Month.October))
                    return false;
                }

                return true;
            }
        }
    }
}