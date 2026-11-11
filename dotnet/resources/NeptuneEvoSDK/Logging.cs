using System;
using GTANetworkAPI;

namespace Redage.SDK
{
    public class nLog
    {
        /// <summary>
        /// Инициализация системы логирования
        /// </summary>
        /// <param name="_reference">Зависимость - Пространство вызова лога, своя пометка в консоли</param>
        /// <param name="_canDebug">Включить или отключить вывод отладочных сообщений для всего пространства</param>
        public nLog(string _reference = null, bool _canDebug = false)
        {
            if (_reference == null) _reference = "Logger";
            Reference = _reference;
            CanDebug = _canDebug;
        }
        /// <summary>
        /// 
        /// </summary>
        public string Reference { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public bool CanDebug { get; set; }

        /// <summary>
        /// Флаги (пометки) строк при выводе в консоль
        /// </summary>
        public enum Type
        {
            /// <summary>
            /// Информаационный лог
            /// </summary>
            Info,
            /// <summary>
            /// Лог ошибки
            /// </summary>
            Warn,
            /// <summary>
            /// 
            /// </summary>
            Error,
            /// <summary>
            /// 
            /// </summary>
            Success
        };

        /// <summary>
        /// Вывести в консоль обычный текст с нужным флагом
        /// </summary>
        /// <param name="text">Выводимый текст</param>
        /// <param name="logType">Флаг. Указывает, как нужно пометить строку</param>
        public void Write(string text, Type logType = Type.Info)
        {
            try
            {
                //Console.ResetColor();
                string ctext = $"{DateTime.Now.ToString("HH':'mm':'ss.fff")} | ";
                switch (logType)
                {
                    case Type.Error:
                        //Console.ForegroundColor = ConsoleColor.Red;
                        ctext += "Ошибка";
                        break;
                    case Type.Warn:
                        //Console.ForegroundColor = ConsoleColor.Yellow;
                        ctext += "Предупреждение";
                        break;
                    case Type.Info:
                        ctext += "Инфо";
                        break;
                    case Type.Success:
                        //Console.ForegroundColor = ConsoleColor.Green;
                        ctext += "Успех";
                        break;
                    default:
                        return;
                }
                //Console.ResetColor();
                Console.WriteLine(ctext + $" | {Reference} | {text}");
            }
            catch (Exception e)
            {
                Console.ResetColor();
                Console.BackgroundColor = ConsoleColor.DarkRed;
                Console.WriteLine("Logger Error:\n" + e.ToString());
                Console.ResetColor();
            }
        }
        /// <summary>
        /// Вывести в консоль отладочный текст с нужным флагом
        /// </summary>
        /// <param name="text">Выводимый текст</param>
        /// <param name="logType">Флаг. Указывает, как нужно пометить строку</param>
        public void Debug(string text, Type logType = Type.Info)
        {
            try
            {
                if (!CanDebug) return;
                Console.ResetColor();
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.Write($"{DateTime.Now.ToString("HH':'mm':'ss.fff")}");
                Console.ResetColor();
                Console.Write($" | ");
                switch (logType)
                {
                    case Type.Error:
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.Write(" Ошибка");
                        break;
                    case Type.Warn:
                        Console.ForegroundColor = ConsoleColor.Yellow;
                        Console.Write(" Предупреждение");
                        break;
                    case Type.Info:
                        Console.Write(" Инфо");
                        break;
                    case Type.Success:
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.Write(" Успех");
                        break;
                    default:
                        return;
                }
                Console.ResetColor();
                Console.Write($" | {Reference} | {text}\n");
            }
            catch (Exception e)
            {
                Console.ResetColor();
                Console.BackgroundColor = ConsoleColor.DarkRed;
                Console.WriteLine("Logger Error:\n" + e.ToString());
                Console.ResetColor();
            }
        }

    }
}
