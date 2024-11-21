namespace PriceHubble.Client.ValueTypes
{
    public class Property
    {
        /// <summary>
        /// Property location
        /// </summary>
        public Location Location { get; set; }

        /// <summary>
        /// Property type
        /// </summary>
        public PropertyType PropertyType { get; set; }

        /// <summary>
        /// The year the property was built
        /// min: 1850, max: <current year> + 3
        /// FR min: 1700
        /// UK min: 1800
        /// ES min: 1921
        /// Not relevant for CZ and SK;
        /// Required for AT, BE, CH, DE, ES, FR, JP, NL
        /// </summary>
        public int? BuildingYear { get; set; }

        /// <summary>
        /// Net living area, in m2 for countries AT, BE, CH, CZ, DE, FR, JP, NL, SK and in sq.ft in UK
        /// Required for all countries, except in UK, min: 10, max: 800, maximum two decimals
        /// CH min: 20
        /// CZ max: 500
        /// FR min: 5, max: 1'000
        /// UK min: 100, max: 10'000
        /// </summary>
        public decimal? LivingArea { get; set; }

        /// <summary>
        /// Total area of the lot/parcel on which the house is standing, in m2 for countries AT, BE, CH, CZ, DE, FR, JP, NL, SK and in acres in UK
        /// Required for house, except in UK, min: 50, max: 5'000, maximum two decimals
        /// BE min: 30, max: 100'000
        /// CZ min: 10
        /// FR min: 10, max: 100'000
        /// UK min: 0, max: 100
        /// </summary>
        public decimal? LandArea { get; set; }

        /// <summary>
        /// Balcony / Terrace area, in m2
        /// min: 0, max: 200, maximum two decimals
        /// FR max: 500
        /// AT, CH max: 10'000
        /// </summary>
        public decimal? BalconyArea { get; set; }

        /// <summary>
        /// Garden area, in m2
        /// Only for apartments, min: 0, max: 200, maximum two decimals
        /// AT, CH max: 10'000
        /// </summary>
        public decimal? GardenArea { get; set; }

        /// <summary>
        /// Volume, in m3
        /// Only for house, only relevant for CH, min: 250, max: 4'000, maximum two decimals
        /// </summary>
        public decimal? Volume { get; set; }

        /// <summary>
        /// Volume standard
        /// Only for house, only relevant for CH
        /// Possible values: building_insurance, sia116, sia416
        /// </summary>
        public decimal? VolumeStandard { get; set; }

        /// <summary>
        /// Building structure
        /// Only relevant for CZ, JP and SK
        /// For JP, possible values are wood, light_weight_steel or reinforced_concrete
        /// For CZ and SK, possible values are wood, stone, panel, mixed, framed_structure, brick
        /// </summary>
        public string? BuildingStructure { get; set; }

        /// <summary>
        /// Number of rooms
        /// min: 1, max: 20
        ///
        /// CZ max: 10
        /// in AT, CH and DE, half rooms are allowed (e.g. 2.5);
        ///
        /// in AT and CH, max: 100;
        ///
        /// All other countries are only allowed integer
        ///
        /// in JP, missing values for numberOfRooms are filled using roomsConfiguration and livingArea.
        /// Not supported in BE, ES, UK
        /// </summary>
        public decimal? NumberOfRooms { get; set; }

        /// <summary>
        /// Whether the kitchen is in a separate room or part of the living area
        /// Only relevant for CZ (example usage: set numberOfRooms = 2 and hasOpenKitchen = true to represent a "2+kk" room layout; set numberOfRooms = 2 and hasOpenKitchen = false to represent a "2+1" room layout)
        /// </summary>
        public bool? HasOpenKitchen { get; set; }

        /// <summary>
        /// Number of bedrooms
        /// min: 1, max: 20
        /// Only supported in BE, ES, UK
        /// Required for UK
        /// </summary>
        public int? NumberOfBedrooms { get; set; }

        /// <summary>
        /// Number of bathrooms
        /// min: 1, max: 5
        /// </summary>
        public int? NumberOfBathrooms { get; set; }
    }
}