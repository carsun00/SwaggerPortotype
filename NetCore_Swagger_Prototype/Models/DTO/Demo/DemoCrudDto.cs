using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace NetCore_Swagger_Prototype.Models.DTO.Demo
{
    public class DemoCrudDto
    {
        /// <summary>
        ///     編碼
        /// </summary>
        [Required]
        public long Id { get; set; }

        /// <summary>
        ///     姓名
        /// </summary>
        [StringLength(100)]
        public string Name { get; set; }

        /// <summary>
        ///     郵件
        /// </summary>
        [EmailAddress]
        public string Email { get; set; }

        /// <summary>
        ///     年齡
        /// </summary>
        [Range(1, 100)]
        public int Age { get; set; }

        /// <summary>
        ///     完成
        /// </summary>
        [DefaultValue(false)]
        public bool IsComplete { get; set; }
    }
}
