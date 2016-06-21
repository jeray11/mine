using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mine.core.Domain.Stores
{
    public class Store : BaseEntity
    {
        /// <summary>
        /// 名称
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 地址
        /// </summary>
        public string Url { get; set; }
        /// <summary>
        /// 是否可以使用ssl
        /// </summary>
        public bool SslEnabled { get; set; }
        /// <summary>
        ///  Gets or sets the store secure URL (HTTPS)
        /// </summary>
        public string SecureUrl { get; set; }
        /// <summary>
        /// Gets or sets the display order
        /// </summary>
        public int DisplayOrder { get; set; }
        /// <summary>
        /// 公司名
        /// </summary>
        public string CompanyName { get; set; }
        /// <summary>
        /// Gets or sets the company address
        /// </summary>
        public string CompanyAddress { get; set; }
        /// <summary>
        /// Gets or sets the store phone number
        /// </summary>
        public string CompanyPhoneNumber { get; set; }
        /// <summary>
        /// Gets or sets the company VAT (used in Europe Union countries)
        /// </summary>
        public string CompanyVat { get; set; }
        /// <summary>
        /// Gets or sets the comma separated list of possible HTTP_HOST values
        /// </summary>
        public string Hosts { get; set; }
    }
}
