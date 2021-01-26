using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Deviser.Demo.Admin.Models
{
    public class Guest
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public Gender Gender { get; set; }
        public string Email { get; set; }
        public bool IsTakePartInDinner { get; set; }
        public FoodType FoodType { get; set; }
    }

    public class FoodType
    {
        private static IList<FoodType> _foodTypes;
        public int Id { get; set; }
        public string Name { get; set; }

        public static IList<FoodType> FoodTypes =>
            _foodTypes ?? new List<FoodType>()
            {
                new FoodType()
                {
                    Id = 1,
                    Name = "Standard"
                },
                new FoodType()
                {
                    Id = 2,
                    Name = "Vegetarian"
                },
                new FoodType()
                {
                    Id = 3,
                    Name = "Low‑Lactose Meal"
                },
                new FoodType()
                {
                    Id = 4,
                    Name = "Gluten Friendly Meal"
                }
            };
    }

    public class Gender
    {
        private static IList<Gender> _genders;
        public int Id { get; set; }
        public string Name { get; set; }

        public static IList<Gender> Genders =>
            _genders ?? new List<Gender>()
            {
                new Gender()
                {
                    Id = 1,
                    Name = "Male"
                },
                new Gender()
                {
                    Id = 2,
                    Name = "Female"
                },
                new Gender()
                {
                    Id = 3,
                    Name = "transgender"
                }
            };
    }
}
