using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;
using UnityEditor.Compilation;
using UnityEngine;

public class LTAHelper
{
    public static string PreprocessScriptAssetTemplate(string pathName, string resourceContent)
    {
        string rootNamespace = null;

        if (Path.GetExtension(pathName) == ".cs")
        {
            rootNamespace = CompilationPipeline.GetAssemblyRootNamespaceFromScriptPath(pathName);
        }

        string content = resourceContent;

        // #NOTRIM# is a special marker that is used to mark the end of a line where we want to leave whitespace. prevent editors auto-stripping it by accident.
        content = content.Replace("#NOTRIM#", "");

        // macro replacement
        string baseFile = Path.GetFileNameWithoutExtension(pathName);

        content = content.Replace("#NAME#", baseFile);
        string baseFileNoSpaces = baseFile.Replace(" ", "");
        content = content.Replace("#SCRIPTNAME#", baseFileNoSpaces);
        content = content.Replace("#SCRIPTNAMEINFO#", baseFileNoSpaces+"Info");

        content = RemoveOrInsertNamespace(content, rootNamespace);

        // if the script name begins with an uppercase character we support a lowercase substitution variant
        if (char.IsUpper(baseFileNoSpaces, 0))
        {
            baseFileNoSpaces = char.ToLower(baseFileNoSpaces[0]) + baseFileNoSpaces.Substring(1);
            content = content.Replace("#SCRIPTNAME_LOWER#", baseFileNoSpaces);
        }
        else
        {
            // still allow the variant, but change the first character to upper and prefix with "my"
            baseFileNoSpaces = "my" + char.ToUpper(baseFileNoSpaces[0]) + baseFileNoSpaces.Substring(1);
            content = content.Replace("#SCRIPTNAME_LOWER#", baseFileNoSpaces);
        }

        return content;
    }

    public static string RemoveOrInsertNamespace(string content, string rootNamespace)
    {
        var rootNamespaceBeginTag = "#ROOTNAMESPACEBEGIN#";
        var rootNamespaceEndTag = "#ROOTNAMESPACEEND#";

        if (!content.Contains(rootNamespaceBeginTag) || !content.Contains(rootNamespaceEndTag))
            return content;

        if (string.IsNullOrEmpty(rootNamespace))
        {
            content = Regex.Replace(content, $"((\\r\\n)|\\n)[ \\t]*{rootNamespaceBeginTag}[ \\t]*", string.Empty);
            content = Regex.Replace(content, $"((\\r\\n)|\\n)[ \\t]*{rootNamespaceEndTag}[ \\t]*", string.Empty);

            return content;
        }

        // Use first found newline character as newline for entire file after replace.
        var newline = content.Contains("\r\n") ? "\r\n" : "\n";
        var contentLines = new List<string>(content.Split(new[] { "\r\n", "\r", "\n" }, StringSplitOptions.None));

        int i = 0;

        for (; i < contentLines.Count; ++i)
        {
            if (contentLines[i].Contains(rootNamespaceBeginTag))
                break;
        }

        var beginTagLine = contentLines[i];

        // Use the whitespace between beginning of line and #ROOTNAMESPACEBEGIN# as identation.
        var indentationString = beginTagLine.Substring(0, beginTagLine.IndexOf("#"));

        contentLines[i] = $"namespace {rootNamespace}";
        contentLines.Insert(i + 1, "{");

        i += 2;

        for (; i < contentLines.Count; ++i)
        {
            var line = contentLines[i];

            if (String.IsNullOrEmpty(line) || line.Trim().Length == 0)
                continue;

            if (line.Contains(rootNamespaceEndTag))
            {
                contentLines[i] = "}";
                break;
            }

            contentLines[i] = $"{indentationString}{line}";
        }

        return string.Join(newline, contentLines.ToArray());
    }
}
