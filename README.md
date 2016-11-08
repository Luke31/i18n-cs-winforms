# i18n-cs
This is a collection of Visual Studio 2015 Applications to demonstrate internationalization in C# Code, WinForms, WPF and WIX installers.
The default language is English and translations to Japanese are added. This approach is recommended so systems other than Japanese or English will see an English interface.

# C-Sharp

## C# Application's default culture
To define the application's default culture (In this case English), the _NeutralResourcesLanguageAttribute_ AssemblyInfo must be set:

**Hint:** Always use _English_ and not a specific English locale such as _English (United States)_

![AssemblyInfo Default Culture](tutorial_img/AssemblyInfo_NeutralResourcesLanguageAttribute.png)

## C# WinForms

*Example Project: WinForms*

This Tutorial is based on Microsofts MSDN Tutorial [Walkthrough: Localizing Windows Forms](https://msdn.microsoft.com/en-us/library/y99d1cd3(v=vs.100).aspx)

Following resources are used for internationalzation:

* Project resources (non-form-based, dialog-boxes, error-messages)
* Form resources (Auto-generated)

**Hint:** For Forms-Property: always use either project OR form resources, not mixed

### Localizable Forms (Form resources)

1. Set project as localizable

	![Set localizable](tutorial_img/1_enablei18n.png)

2. Open Form to translate

	![Form to translate](tutorial_img/1_formDefaultLanguage.png)

3. Change Property Language of Form to Japanese

	![Form in Japanese](tutorial_img/1_formJapanese.png)

4. Set Text of desired Element (e.g. Button) to translated new Text.

	![Form in Japanese with Japanese text](tutorial_img/1_formJapaneseTextEdited.png)

### Localizable Error-message and dialog-boxes (Project resources)

This part is also applicable to simple Console-applications

1. Add new Resource-file to the Project

	**Hint:** This file is the fallback for the current default language (English), so the text in this file should be English.

	![New Project resources](tutorial_img/2_projectResourceName.png)
	
2. Enter a new string with a default English text.

	![Japanese Text](tutorial_img/2_projectResEn.png)

3. Repeat step 1 and 2 with a new file named _WinFormStrings.ja.resx_

	![Japanese Text](tutorial_img/2_projectResJp.png)

4. To access the manually added resources (e.g. on a button-click) use the following code:

		using System.Resources;
		
		...
		
		// Access resource by Getter (Recommended as by http://stackoverflow.com/a/14503044/2003325)
		MessageBox.Show(WinFormStrings.errorInsuffMemory);

**Hint:** To force the program to start in a specific locale, uncomment one of these lines in _Program.cs_ 

	//Thread.CurrentThread.CurrentUICulture = new CultureInfo("ja-JP"); //Japanese
    //Thread.CurrentThread.CurrentUICulture = new CultureInfo("en-GB"); //English (Default of this project)
	
**Hint:** Instead of creating a new default resources-file the existing default Resources.resx file under Properties may be used. (See [this stackoverflow answer](http://stackoverflow.com/a/1129152/2003325)).
For this, only a new Resources file for the additional language Japanese must be added: _Resources.ja.resx_.
However, this approach results that ALL strings will be in one file and can't be distinguished. So separated Resources-files are strongly recommended!
	
## C# Code

*Example Project: CodeDefaultEnglish*

See WinForms -> Localizable Error-message and dialog-boxes (Project resources)

## C# Fallback to Satellite Assembly
Usually the application fallback is the main assembly if the requested UI Culture cannot be found. 
However, if another culture should be used as a fallback, you may do so by defining a satellite assembly for fallback:

See [Packaging and Deploying Resources in Desktop Apps](https://msdn.microsoft.com/en-us/library/sb6a8618(v=vs.110).aspx) -> Ultimate Fallback to Satellite Assembly

Set in Code or in AssemblyInfo.cs

	[assembly:NeutralResourcesLanguage("en", UltimateResourceFallbackLocation.Satellite)]
	
There are various combinations of default language and satellite languages (The specfic VS-Project in bold):

* **CodeDefaultEnglish** - Default language is English, Satellite Japanese -> Fallback will be English (e.g. German user) **Recommended**
* **CodeDefaultJapanese** - Default language is Japanese, Satellite English -> Fallback will be Japanese (e.g. German user)
* **CodeJapaneseDefaultSatelliteEn** - Default language is Japanese, Satellite English AND Japanese -> Ultimate Fallback on English -> Fallback will be English

## C# WPF

*Example Project: WPF*

This Tutorial is NOT based on Microsofts MSDN Tutorial [WPF Globalization and Localization Overview](https://msdn.microsoft.com/en-us/library/ms788718(v=vs.110).aspx)

Instead we will use the free WPFLocalizationExtension (https://github.com/SeriousM/WPFLocalizationExtension) under the [Ms-PL license](https://tldrlegal.com/license/microsoft-public-license-(ms-pl)) in combinatino with Resources .resx files.

Follow the very simple Tutorial [WPF: Localization using Resources and Localization Extension](http://www.broculos.net/2014/04/wpf-localization-using-resources-and.html#.WBlSQvqLSUk) and mind the hints below:

* You can install the WPFLocalizationExtension using NuGet:

		Install-Package WpfLocalizeExtension

* For created Resource .resx files, set the Access Modifier to _Public_

* Specify the current language to the System langauge in starting your application:

		LocalizeDictionary.Instance.SetCurrentThreadCulture = true;
		
		//Set Extension Culture to System culture:
		LocalizeDictionary.Instance.Culture = new CultureInfo(CultureInfo.CurrentUICulture.Name);
		
* How to prepare your XAML, you may see here [Usage - Preparing the XAML code](https://wpflocalizeextension.codeplex.com/wikipage?title=Preparing%20the%20XAML%20code&referringTitle=Documentation)

		<Window xmlns:lex="http://wpflocalizeextension.codeplex.com"
			lex:LocalizeDictionary.DesignCulture="en"
			lex:ResxLocalizationProvider.DefaultAssembly="WPF"
			lex:ResxLocalizationProvider.DefaultDictionary="LocResources">
			<!-- Some controls -->
		</Window>
		
* How to access the resoruces in the XAML you may find here: [Usage - Keys](https://wpflocalizeextension.codeplex.com/wikipage?title=Keys&referringTitle=Documentation)

* If you would like to access Resources from different Assemblies in the XAML, look here: [Usage - Multiple assemblies and dictionaries](https://wpflocalizeextension.codeplex.com/wikipage?title=Multiple%20assemblies%20and%20dictionaries)
	
* Look at the [WPFLocalizationExtension Wiki](https://wpflocalizeextension.codeplex.com/documentation) for further questions

### Additional helpful features

* SizeToContent - Make window size automatic depending on content

		<Window SizeToContent="WidthAndHeight">

* SharedSizeGroup - The elements have the same size from the biggest element

		<Grid.ColumnDefinitions>
		  <ColumnDefinition x:Uid="ColumnDefinition_1" />
		  <ColumnDefinition x:Uid="ColumnDefinition_2" />
		  <ColumnDefinition x:Uid="ColumnDefinition_3" **SharedSizeGroup="Buttons"** />
		  <ColumnDefinition x:Uid="ColumnDefinition_4" **SharedSizeGroup="Buttons"** />
		  <ColumnDefinition x:Uid="ColumnDefinition_5" **SharedSizeGroup="Buttons"** />
		</Grid.ColumnDefinitions>


	
## C# Translate resource files
Potentially useful tool for translating resource files:

Zeta Resource Editor (https://www.zeta-resource-editor.com/index.html)

## C# Tree-View of resource-files with language
How to get a treeview of culture-specific resources

Unload the project
Edit the corresponding csproj file
Locate the tags of the resources and rewrite them using the DependentUpon syntax:

	<EmbeddedResource Include="Strings.de.resx">
	  <SubType>Designer</SubType>
	  <DependentUpon>Strings.resx</DependentUpon>
	</EmbeddedResource>
	<EmbeddedResource Include="Strings.resx">
	  <Generator>ResXFileCodeGenerator</Generator>
	  <LastGenOutput>Strings.Designer.cs</LastGenOutput>
	</EmbeddedResource>
	
# Visual C++

*Example Project: Cpp*

Basic for localization in Visual C++ are resources in .dll or .exe [Implementing Globalization / Multilingual feature in win32 API application](http://stackoverflow.com/a/1654791/2003325)

Each resource contains a language identifier. The same resource with the same name may exist with different languages.
To access these resources the Windows SDK LoadString, LoadBitmap etc. may be used.
	
Strings in your code should be in a _String Table resource_ and retrieved using LoadString (or more neutrally FindResource).

1. Create resource and add String Tables for both languages English and Japanese

	![String Table English](tutorial_img/3_stringTableEN.png)
	
	![String Table Japanese](tutorial_img/3_stringTableJP.png)

2. Create the following wrapper function using _LoadString_ for loading a string from a resource (Source: [Stackoverflow post: c++, Win32 LoadString wrapper](http://stackoverflow.com/a/33336980/2003325)):

		std::wstring LoadStringW(unsigned int id)
		{
			const wchar_t* p = nullptr;
			int len = ::LoadStringW(nullptr, id, reinterpret_cast<LPWSTR>(&p), 0);
			if (len > 0)
			{
				return std::wstring(p, static_cast<size_t>(len));
			}
			// Return empty string; optionally replace with throwing an exception.
			return std::wstring();
		}

3. Enable your application to support Unicode output in console as well as in stdout, use the _wmain_ signature and set the following lines at the beginning of your application:

		int wmain(int argc, wchar_t* argv[])
		{
			_setmode(_fileno(stdout), _O_U16TEXT); //_O_WTEXT
			//stdout may now be written to file (First character must be ASCII if output is written to file)
			std::wcout << L"Enabling Unicode support" << std::endl;
			...
		}
	
4. Load and output your string:

		//Load multi-lang resource
		std::wstring str = LoadStringW(IDS_HW);

		//Output using wprintf
		wprintf(str.c_str());
		std::wcout << std::endl;

		//Output using wcout
		std::wcout << str << std::endl;
		
* **Hint:** Following includes are required:

		#include "stdafx.h"
		#include "resource.h" //Enables us to load our resources -> multi-lang strings
		#include <string>
		#include <iostream>
		#include <io.h> //for setting outputmode UNICODE
		#include <fcntl.h> //contains _O_U16TEXT

# Python (IronPython 2.7)

*Example project: IronPythonModule and IronPythonApplication*

This tutorial is based on [IronPython Python 2.7 - Internationalizing your programs and modules](https://ironpython-test.readthedocs.io/en/latest/library/gettext.html#internationalizing-your-programs-and-modules)
Another Tutorial (Warning Python 3!) is [Translate Your Python 3 Program with the gettext Module](http://inventwithpython.com/blog/2014/12/20/translate-your-python-3-program-with-the-gettext-module/)
[Unicode mess](http://www.wefearchange.org/2012/06/the-right-way-to-internationalize-your.html)

1. Mark Strings with 

		_('...Text...')
	
2. Run _pygettext.py_ (Similar to GNU xgettext) from _C:\Python27\Tools\i18n_ on your IronPython-file
	
	**Hint:** IronPython does not contain a Tools\i18n folder! Use a regular Python 2.7 instance instead!

		pygettext.py sample/IronPython.py
	
3. Copy the generated _messages.pot_ template file and transate the strings to a new file called _messages.po_ (Yes _.po_, not _.pot_ - make sure the file is saved in Unicode)

	**Hint:** You may use the Tool [Poedit](https://poedit.net/) to translate your strings:
	
	![Open pot Template](tutorial_img/4_poedit_fromtemplate.png)
	
	![Edit text and save as messages.po](tutorial_img/4_poedit_text.png)

4. Convert the .po-file to a .mo-binary-file using _msgfmt.py_ in _C:\Python27\Tools\i18n_

	**Hint:** If you've used Poedit, the tool has already done this for you :)

		msgfmt.py messages
		
The next steps depend on whether you would like to localize your whole application or just a module
		
5. Localizing module (Example-Module named _sample_)

		import gettext
		t = gettext.translation('IronPython', '/usr/share/locale')
		_ = t.ugettext

5. Localizing application

	
		
* Change languages on the fly:

		import gettext

		langEn = gettext.translation('IronPython', languages=['en'])
		langJa = gettext.translation('IronPython', languages=['ja'])

		# start by using language1
		langEn.install()
		
**Hint:** If file contains UTF-8 characters, put this at top of file:
	
	# -*- coding: utf-8 -*-