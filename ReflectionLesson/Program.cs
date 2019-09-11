using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;

namespace ReflectionLesson
{
    class Program
    {
        static void Main(string[] args)
        {
            var assembly = Assembly.LoadFile(@"C:\Users\МолдашеваА\source\repos\TestReflectionLesson\TestReflectionLesson\bin\Debug\TestReflectionLesson.exe");
            Console.WriteLine($"{assembly.FullName}");

            var types = assembly.GetTypes();
            foreach(var type in types)
            {
                Console.WriteLine("***********");
                //var programType = typeof(Program);
                //var name = "Имя";
                //var strType = name.GetType();
                var typeIn = type.IsClass ? "класс" : "другой тип";
                Console.WriteLine($"{type.Name}, {typeIn}");

                foreach (var memberInfo in type.GetMembers())
                {
                    if (memberInfo is MethodInfo)
                    {
                        var methodInfo = memberInfo as MethodInfo;
                        Console.WriteLine($"{methodInfo.Name}, {methodInfo.ReturnType}");
                        foreach (var parameter in methodInfo.GetParameters())
                        {
                            Console.WriteLine($"{parameter.ParameterType},{parameter.Name}");
                        }

                        if(type.Name == "MessageService" && memberInfo.Name == "Dis")
                        {
                            object messageService = Activator.CreateInstance(type, new object[] { "cooбщение" });
                            methodInfo.Invoke(messageService, new object[] { });
                        }

                    }
                
                         else if(memberInfo is ConstructorInfo)
                {
                         var constructorInfo = memberInfo as ConstructorInfo;
                         Console.WriteLine($"{constructorInfo.Name}");
                         foreach (var parameter in constructorInfo.GetParameters())
                          {
                               Console.WriteLine($"{parameter.ParameterType},{parameter.Name}");
                          }
                }
                }
            
        }
            Console.ReadLine();
        }
    }
}
