using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;

namespace ProjectApi.API.DTOs
{
    /// <summary>
    /// Повна анкета STEM-опитування (як на фото)
    /// </summary>
    public class StudentSurveyFullDto
    {
        /// <summary>ПІБ (обов'язково)</summary>
        [Required]
        public string FullName { get; set; } = string.Empty;
        /// <summary>Клас (обов'язково, max 32 символи)</summary>
        [Required, MaxLength(32)]
        public string Class { get; set; } = string.Empty;
        /// <summary>Навчальний заклад (Id школи з БД, обов'язково)</summary>
        [Required]
        public int SchoolId { get; set; }
        /// <summary>Стать (опціонально: чоловіча/жіноча)</summary>
        public string? Gender { get; set; }

        // --- МАТЕМАТИКА ---
        /// <summary>Математика: нудна (1) — цікава (7)</summary>
        [Range(1,7)] public int? Math_Boring_Interesting { get; set; }
        /// <summary>Математика: неприваблива (1) — приваблива (7)</summary>
        [Range(1,7)] public int? Math_Unattractive_Attractive { get; set; }
        /// <summary>Математика: повсякденна (1) — дивовижна (7)</summary>
        [Range(1,7)] public int? Math_Everyday_Amazing { get; set; }
        /// <summary>Математика: незахоплюча (1) — захоплюча (7)</summary>
        [Range(1,7)] public int? Math_Unexciting_Exciting { get; set; }
        /// <summary>Математика: нічого не означає (1) — багато значить (7)</summary>
        [Range(1,7)] public int? Math_Meaningless_Meaningful { get; set; }

        // --- НАУКА ---
        [Range(1,7)] public int? Science_Everyday_Amazing { get; set; }
        [Range(1,7)] public int? Science_Unattractive_Attractive { get; set; }
        [Range(1,7)] public int? Science_Unexciting_Exciting { get; set; }
        [Range(1,7)] public int? Science_Meaningless_Meaningful { get; set; }
        [Range(1,7)] public int? Science_Boring_Interesting { get; set; }

        // --- ІНЖЕНЕРІЯ ---
        [Range(1,7)] public int? Eng_Unattractive_Attractive { get; set; }
        [Range(1,7)] public int? Eng_Everyday_Amazing { get; set; }
        [Range(1,7)] public int? Eng_Meaningless_Meaningful { get; set; }
        [Range(1,7)] public int? Eng_Unexciting_Exciting { get; set; }
        [Range(1,7)] public int? Eng_Boring_Interesting { get; set; }

        // --- ТЕХНОЛОГІЇ ---
        [Range(1,7)] public int? Tech_Unattractive_Attractive { get; set; }
        [Range(1,7)] public int? Tech_Meaningless_Meaningful { get; set; }
        [Range(1,7)] public int? Tech_Boring_Interesting { get; set; }
        [Range(1,7)] public int? Tech_Unexciting_Exciting { get; set; }
        [Range(1,7)] public int? Tech_Everyday_Amazing { get; set; }

        // --- КАР'ЄРА ---
        [Range(1,7)] public int? Career_Meaningless_Meaningful { get; set; }
        [Range(1,7)] public int? Career_Boring_Interesting { get; set; }
        [Range(1,7)] public int? Career_Unexciting_Exciting { get; set; }
        [Range(1,7)] public int? Career_Everyday_Amazing { get; set; }
        [Range(1,7)] public int? Career_Unattractive_Attractive { get; set; }
    }
} 