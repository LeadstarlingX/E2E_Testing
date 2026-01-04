namespace EndToEndTesting.Data
{
    public static class Constants
    {
        public const string BaseUrl = "https://www.saucedemo.com/";
        public const int DefaultInteractionDelay = 500;
        public const int ExplicitWaitTimeout = 10;
        public const int ImplicitWaitTimeout = 5;

        public static class Users
        {
            public const string StandardUser = "standard_user";
            public const string LockedOutUser = "locked_out_user";
            public const string ProblemUser = "problem_user";
            public const string PerformanceGlitchUser = "performance_glitch_user";
            public const string ErrorUser = "error_user";
            public const string VisualUser = "visual_user";
            public const string SecretSauce = "secret_sauce";
        }

        public static class Products
        {
            public const string Backpack = "Sauce Labs Backpack";
            public const string BikeLight = "Sauce Labs Bike Light";
            public const string BoltTShirt = "Sauce Labs Bolt T-Shirt";
            public const string FleeceJacket = "Sauce Labs Fleece Jacket";
            public const string Onesie = "Sauce Labs Onesie";
            public const string RedTShirt = "Test.allTheThings() T-Shirt (Red)";
        }

        public static class Prices
        {
            public const double Backpack = 29.99;
            public const double BikeLight = 9.99;
            public const double BoltTShirt = 15.99;
            public const double FleeceJacket = 49.99;
            public const double Onesie = 7.99;
            public const double RedTShirt = 15.99;
        }
    }
}
