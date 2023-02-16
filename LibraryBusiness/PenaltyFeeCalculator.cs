using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using LibraryConfigUtilities;

namespace LibraryBusiness
{
    public class PenaltyFeeCalculator{
        
        private  readonly List<Country> settingList = new LibrarySetting().LibrarySettingList;
        
        public PenaltyFeeCalculator() {
            
        }

        public String Calculate(DateTime startDate, DateTime endDate, string country){
            var setting = settingList.FirstOrDefault(x => x.CountryCode == country);

            if (setting == null)
            {
                throw new ArgumentException($"Country '{country}' is not supported");
            }

            if (endDate < startDate)
            {
                throw new ArgumentException("End date cannot be earlier than start date");
            }

            var penaltyAppliesAfter = setting.PenaltyAppliesAfter;
            var dailyPenaltyFee = setting.DailyPenaltyFee;
            var holidays = setting.HolidayList.Select(h => h.Date).ToList();
            var weekends = setting.WeekendList.Select(w => (DayOfWeek)w).ToList();

            var daysLate = GetBusinessDaysBetween(startDate, endDate, holidays,weekends);
            var penalty = (daysLate > penaltyAppliesAfter) ? (daysLate - penaltyAppliesAfter) * dailyPenaltyFee : 0m;
            return penalty.ToString("0.00");
        }

        private int GetBusinessDaysBetween(DateTime startDate, DateTime endDate, List<DateTime> holidays, List<DayOfWeek> weekends)
        {
            var days = 0;
            var date = startDate;
            while (date <= endDate)
            {
                if (!weekends.Contains(date.DayOfWeek) && !holidays.Contains(date))
                {
                    days++;
                }
                date = date.AddDays(1);
            }
            return days;
        }
    }
}
