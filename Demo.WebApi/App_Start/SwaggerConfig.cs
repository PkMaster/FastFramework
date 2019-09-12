using System.Web.Http;
using WebActivatorEx;
using Demo.WebApi;
using Swashbuckle.Application;
using Swashbuckle.Swagger;
using System.Web.Http.Description;
using System.Collections.Generic;
using System.Linq;
using Demo.WebApi.App_Start;

[assembly: PreApplicationStartMethod(typeof(SwaggerConfig), "Register")]

namespace Demo.WebApi
{
    /// <summary>
    /// swagger�ĵ�������Ϣ
    /// </summary>
    public class SwaggerConfig
    {
        /// <summary>
        /// ע��swagger���������Ϣ
        /// </summary>
        public static void Register()
        {
            var thisAssembly = typeof(SwaggerConfig).Assembly;

            GlobalConfiguration.Configuration
                .EnableSwagger(c =>
                {
                    //����swagger��ʾ�İ汾�źͱ���
                    c.SingleApiVersion("v1", "Api�����ĵ�");

                    //���ýӿ�����xml·����ַ
                    var webApiXmlPath = string.Format("{0}/bin/Demo.WebApi.xml", System.AppDomain.CurrentDomain.BaseDirectory);
                    c.IncludeXmlComments(webApiXmlPath);

                    //���ýӿ�����xml·����ַ
                    var applicationXmlPath = string.Format("{0}/bin/Demo.Application.xml", System.AppDomain.CurrentDomain.BaseDirectory);
                    c.IncludeXmlComments(applicationXmlPath);

                    //���ýӿ�����xml·����ַ
                    var coreXmlPath = string.Format("{0}/bin/Demo.Core.xml", System.AppDomain.CurrentDomain.BaseDirectory);
                    c.IncludeXmlComments(coreXmlPath);

                    //�������������
                    c.CustomProvider((defaultProvider) => new SwaggerControllerDescProvider(defaultProvider, webApiXmlPath));
                })
                .EnableSwaggerUi(c =>
                {
                    //���غ�����js�ļ���ע�� swagger_lang_cn.js �ļ����Ա�������Ϊ��Ƕ�����Դ����
                    c.InjectJavaScript(System.Reflection.Assembly.GetExecutingAssembly(), "Demo.WebApi.Scripts.swagger_lang_cn.js");
                    //�������token������ͷjs�ļ�
                    c.InjectJavaScript(System.Reflection.Assembly.GetExecutingAssembly(), "Demo.WebApi.Scripts.token.js");
                });
        }
    }
}
