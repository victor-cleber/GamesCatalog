using System;
using System.ComponentModel.DataAnnotations;

namespace GamesCatalogAPI.InputModel {
    public class GameInputModel {
        [Required]
        [StringLength(100, MinimumLength = 3, ErrorMessage = "The name of the game is required within a minimum of 3 characters")]
        public string Name {
            get; set;
        }

        [Required]
        [StringLength(100, MinimumLength = 3, ErrorMessage = "The name of the game is required within a minimum of 3 characters")]
        public string Producer {
            get; set;
        }

        [Required]
        [Range(1, 1000, ErrorMessage = "The price has to be a minimum of $1 and a maximum of $1000")]
        public double Price {
            get; set;
        }
    }
}
