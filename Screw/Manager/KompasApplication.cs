using Kompas6API5;
using Kompas6Constants3D;
using Screw.Error;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Screw.Manager

{   /// <summary>
    /// Kompas application manager class
    /// </summary>
    public class KompasApplication
    {
        

            /// <summary>
            /// Get Kompas 3D object
            /// </summary>
            public KompasObject KompasObject
            {
                get;
                private set;
            }

            /// <summary>
            /// 3D document сборки
            /// </summary>
            public ksDocument3D Document3D
            {
                get;
                private set;
            }

            /// <summary>
            /// Screw part of build
            /// </summary>
            public ksPart ScrewPart
            {
                get;
                private set;
            }

            /// <summary>
            /// Шаг резьбы
            /// </summary>
            public double ThreadStep
            {
                get;
                set;
            }

            /// <summary>
            /// Параметры фигуры
            /// </summary>
            public List<double> Parameters
            {
                get;
                set;
            }

            /// <summary>
            /// Код последней ошибки
            /// </summary>
            public Error.ErrorCodes LastErrorCode
            {
                get;
                private set;
            }

            /// <summary>
            /// Create object of application
            /// </summary>
            public KompasApplication()
            {
                if (!GetActiveApp())
                {
                    if (!CreateNewApp())
                    {
                        return;
                    }
                }
            }

            /// <summary>
            /// Create 3D document
            /// </summary>
            /// <returns>true - если операция прошла успешно; false - в случае ошибки</returns>
            public bool CreateDocument3D()
            {
                Document3D = (ksDocument3D)KompasObject.Document3D();

                // Create build
                if (!Document3D.Create(false/*visible*/, false/*build*/))
                {
                    LastErrorCode = ErrorCodes.Document3DCreateError;
                    return false;
                }

                // Create screw detail on 3D document
                ScrewPart = (ksPart)Document3D.GetPart((short)Part_Type.pTop_Part);

                if (ScrewPart == null)
                {
                    LastErrorCode = ErrorCodes.Document3DGetPartError;
                    return false;
                }

                return true;
            }

        /// <summary>
        /// Подключиться к активнопу приложению Компас
        /// </summary>
        /// <returns>true - если операция прошла успешно; false - в случае ошибки</returns>
        private bool GetActiveApp()
            {
                // Try приложение активно
                if (KompasObject == null)
                {
                    try
                    {
                        KompasObject = (KompasObject)Marshal.GetActiveObject("KOMPAS.Application.5");
                    }
                    catch
                    {
                        return false;
                    }
                }

                // Else приложение не запущено -- return
                if (KompasObject == null)
                {
                    return false;
                }

                KompasObject.Visible = true;
                KompasObject.ActivateControllerAPI();

                return true;
            }

        /// <summary>
        /// Запуск приложения Компас
        /// </summary>
        /// <returns>true - если операция прошла успешно; false - в случае ошибки</returns>
        private bool CreateNewApp()
            {
                Type t = Type.GetTypeFromProgID("KOMPAS.Application.5");
                KompasObject = (KompasObject)Activator.CreateInstance(t);

                if (KompasObject == null)
                {
                    LastErrorCode = ErrorCodes.KompasApplicationCreatingError;
                    return false;
                }

                KompasObject.Visible = true;
                KompasObject.ActivateControllerAPI();

                return true;
            }

            /// <summary>
            /// Закрыть программу
            /// </summary>
            public void DestructApp()
            {
                KompasObject.Quit();
                KompasObject = null;

                Document3D = null;
                ScrewPart = null;

                LastErrorCode = ErrorCodes.OK;
            }
    }
}

