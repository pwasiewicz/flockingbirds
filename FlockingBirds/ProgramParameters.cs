using System.Windows.Forms.VisualStyles;

namespace FlockingBirds
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;
    using System;
    using ParametersCore;
    using FlockingSimulation.Setup;
    using NLog;


    internal class ProgramParameters
    {
        private static readonly Logger Logger = LogManager.GetCurrentClassLogger();

        private static readonly object LockObject = new object();

        private static IEnumerable<MethodInfo> methods;

        internal static bool IsAskingForHelp(IEnumerable<string> args)
        {
            var list = args as IList<string> ?? args.ToList();

            return list.Count == 1 && list.Single().Equals("-help");
        }

        internal static void PrintHelp()
        {
            Console.WriteLine("FlockingBirds - Help");
            Console.WriteLine("Parameters descriptions for FlockingBirds program: ");

            foreach (var parameterMethod in GetParameterMethods())
            {
                PrintHelp(parameterMethod);
            }
        }

        private static void PrintHelp(MemberInfo parameterMethod)
        {
            var baseAttr = GetAttribute<ParameterAttribute>(parameterMethod);
            var descAttr = GetAttribute<ParameterDescriptionAttribute>(parameterMethod);

            Console.WriteLine("\t{0}:", baseAttr.Name);
            Console.WriteLine("\t\tExpecting value: {0}", baseAttr.ExpectValue ? "Yes" : "No");
            Console.WriteLine("\t\tDescription: {0}",
                descAttr != null ? descAttr.Description : "Parameter description is missing.");
            Console.WriteLine();
        }

        internal static void GetSetup(IFlockingSetup setup, IEnumerable<string> args)
        {
            var argsListed = args as IList<string> ?? args.ToList();

            foreach (var parameterMethod in GetParameterMethods())
            {
                GetSetup(parameterMethod, setup, argsListed);
            }
        }

        private static T GetAttribute<T>(MemberInfo method) where T : Attribute
        {
            return method.GetCustomAttributes(typeof (T)).OfType<T>().FirstOrDefault();
        }

        private static void GetSetup(MethodBase parameterMethod, IFlockingSetup setup, IEnumerable<string> args)
        {
            var baseAttr = GetAttribute<ParameterAttribute>(parameterMethod);

            var key = baseAttr.Name.StartsWith("-") ? baseAttr.Name : string.Format("-{0}", baseAttr.Name);
            var value = baseAttr.Default;

            var argsList = args as List<string> ?? args.ToList();
            var parameterPosition = argsList.IndexOf(key);

            if (parameterPosition < 0)
            {
                return;
            }

            var lastParamPosition = argsList.LastIndexOf(key);
            if (lastParamPosition != parameterPosition)
            {
                Logger.Fatal("Parameter {0} is specified more than once.", key);

                throw new InvalidOperationException(string.Format("Parameter {0} specified more than once.", key));
            }

            if (baseAttr.ExpectValue)
            {
                if (parameterPosition + 1 < argsList.Count)
                {
                    value = argsList[parameterPosition + 1];
                }
                else
                {
                    Logger.Fatal("Parameter {0} doesn't have a value.", key);

                    throw new InvalidOperationException("Provided key doesn't have value.");
                }
            }

            try
            {
                parameterMethod.Invoke(null, new object[] {setup, value});
            }
            catch (Exception ex)
            {
                Logger.Fatal("Couldn't resolve {0} parameter value (\"{1}\"). Invoking resolving method failed with message: {2}.", key, value, ex.Message);

                throw;
            }
        }

        private static IEnumerable<MethodInfo> GetParameterMethods()
        {
            lock (LockObject)
            {
                if (methods != null)
                {
                    return methods;
                }

                var programType = typeof (Program);
                methods = programType.GetMethods(BindingFlags.Static | BindingFlags.Public)
                    .Where(mi => mi.GetCustomAttributes(typeof (ParameterAttribute)).Any());
            }

            return methods;
        } 
    }
}
