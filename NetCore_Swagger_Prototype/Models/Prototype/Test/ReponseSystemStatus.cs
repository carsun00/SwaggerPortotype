using System;

namespace NetCore_Swagger_Prototype.Models.Prototype.Test
{
    /// <summary>
    ///     Repronse Api Service Test.
    ///     回應API服務的測試.
    ///     
    ///     Range: DB connection, API Servers Test.
    ///     範圍：DB連線、API服務測試。
    ///     
    /// </summary>
    public class ReponseSystemStatus
    {
        /// <summary>
        ///     Test function name.
        ///     測試的相關功能名稱
        /// </summary>
        public string APIName { get; set; }

        /// <summary>
        ///     Execution time.
        ///     執行時間
        /// </summary>
        public DateTime Date { get; set; }

        /// <summary>
        ///     Test Result.
        ///     測試結果
        /// </summary>
        public string Message { get; set; }
    }
}
