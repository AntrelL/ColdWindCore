using System.CodeDom;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.IO;
using System.Reflection;

namespace ColdWind.Core.Editor
{
    public static class FileGenerationTool
    {
        public static void GenerateConstantClass<T>(
            string name, string @namespace, string folderPath, Dictionary<string, T> constants)
        {
            CodeCompileUnit compileUnit = new();
            CodeNamespace codeNamespace = new(@namespace);

            CodeTypeDeclaration classDeclaration = new(name)
            {
                IsClass = true,
                TypeAttributes = TypeAttributes.Public
            };

            foreach (var constant in constants)
            {
                classDeclaration.Members.Add(new CodeMemberField(typeof(T), constant.Key)
                {
                    Attributes = MemberAttributes.Public | MemberAttributes.Const,
                    InitExpression = new CodePrimitiveExpression(constant.Value)
                });
            }

            codeNamespace.Types.Add(classDeclaration);
            compileUnit.Namespaces.Add(codeNamespace);

            SaveFile(compileUnit, name, folderPath, false);
        }

        public static void SaveFile(
            CodeCompileUnit compileUnit, string fileName, string folderPath, bool blankLinesBetweenMembers)
        {
            CodeDomProvider provider = CodeDomProvider.CreateProvider("CSharp");
            string filePath = $"{folderPath}{fileName}.cs";

            if (Directory.Exists(folderPath) == false)
                Directory.CreateDirectory(folderPath);

            using (StreamWriter streamWriter = new(filePath))
            {
                var indentedTextWriter = new IndentedTextWriter(streamWriter);
                var options = new CodeGeneratorOptions
                {
                    BracingStyle = "C",
                    BlankLinesBetweenMembers = blankLinesBetweenMembers
                };

                provider.GenerateCodeFromCompileUnit(compileUnit, indentedTextWriter, options);
                indentedTextWriter.Close();
            }
        }
    }
}
