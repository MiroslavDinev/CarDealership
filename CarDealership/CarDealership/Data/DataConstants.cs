namespace CarDealership.Data
{
    public class DataConstants
    {
        public class Car
        {
            public const int CarBrandMaxLength = 20;
            public const int CarBrandmMinLength = 2;
            public const int CarModelMaxLength = 20;
            public const int CarModelMinLength = 2;
            public const int CarDescriptionMinLength = 10;
            public const int CarYearMaxValue = 2050;
            public const int CarYearMinValue = 2000;
        }

        public class Category
        {
            public const int NameMaxLength = 25;
        }

        public class Dealer
        {
            public const int MaxNameLength = 20;
            public const int MinNameLength = 2;
            public const int MinPhoneNumberLength = 6;
            public const int MaxPhoneNumberLength = 20;

            
        }
    }
}
