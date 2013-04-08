Sitecore.PullUpFields
=====================

Reuse Sitecore Data Template Fields by Pulling Them Up Into a Base Template

Description
-----------

This module -- in the spirit of the 'Pull Up Field' refactoring technique coined by leading refactoring expert Martin Fowler -- arms developers with two context menu options for pulling up fields from a source data template into a new or existing base template.

Installation
------------

Install /Packages/Sitecore Pull Up Fields-1.0.0.0.zip in Sitecore.

Do I need to do anything else after installing the package?
-----------------------------------------------------------

Nope.  The module should be ready to go after installing the package.

Installed Assets
----------------

The installation package installs the following:

Items
-----

The following items are installed into the core database:

* /sitecore/content/Applications/Content Editor/Context Menues/Default/Pull Up Fields Into New Base Template
* /sitecore/content/Applications/Content Editor/Context Menues/Default/Pull Up Fields Into Existing Base Template

The items above should be the first items in the /sitecore/content/Applications/Content Editor/Context Menues/Default folder -- for aesthetics.

Files
-----

* ~/bin/Sitecore.PullUpFields.dll
* ~/App_Config/Include/Sitecore.PullUpFields.config
 
What versions of Sitecore will this work on?
--------------------------------------------

I've testing this on 6.4.1, 6.5 and 6.6.

What version of .NET is required?
---------------------------------

.NET 4.0