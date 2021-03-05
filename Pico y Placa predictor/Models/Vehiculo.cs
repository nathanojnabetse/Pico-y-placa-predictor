using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Pico_y_Placa_predictor.Models
{
    /// <summary>
    /// Clase con los datos de circulación de un automovil
    /// </summary>
    public class Automovil
    {
        [Required]
        [StringLength(8, ErrorMessage = "Longitud máxima 8 caracteres | Formato de placa: XXX-0000")]
        [Display(Name = "Placa")]
        public string placa { get; set; }
        [Required]
        [StringLength(10, ErrorMessage = "Longitud máxima 10 caracteres | Formato de fecha: dd/MM/yyy")]
        [Display(Name = "Fecha")]
        public string fecha { get; set; }
        [Required]
        [StringLength(5, ErrorMessage = "Longitud máxima 5 caracteres | Formato de hora: 24:00")]
        [Display(Name = "hora")]
        public string hora { get; set; }
        public string mensaje { get; set; }
    }
}
