using System.ComponentModel.DataAnnotations;

namespace RecipesApp.Models
{
    public class Recipe
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Название обязательно")]
        [Display(Name = "Название")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Кухня обязательна")]
        [Display(Name = "Кухня")]
        public string Cuisine { get; set; }

        [Range(1, 600, ErrorMessage = "Время готовки от 1 до 600 минут")]
        [Display(Name = "Время готовки (мин)")]
        public int CookingTime { get; set; }

        [Range(1, 5, ErrorMessage = "Сложность от 1 до 5")]
        [Display(Name = "Сложность")]
        public int Difficulty { get; set; }

        [Display(Name = "Описание")]
        [StringLength(500, ErrorMessage = "Описание не более 500 символов")]
        public string Description { get; set; }
    }
}
