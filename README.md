# Analogy Loggers Tree 


![.NET Core Desktop](https://github.com/Analogy-LogViewer/Analogy.LogViewer.LoggersTree/workflows/.NET%20Core%20Desktop/badge.svg)
<a href="https://github.com/Analogy-LogViewer/Analogy.LogViewer.LoggersTree/issues">
    <img src="https://img.shields.io/github/issues/Analogy-LogViewer/Analogy.LogViewer.LoggersTree"  alt="Issues" />
</a>
![GitHub closed issues](https://img.shields.io/github/issues-closed-raw/Analogy-LogViewer/Analogy.LogViewer.LoggersTree)
<a href="https://github.com/Analogy-LogViewer/Analogy.LogViewer.LoggersTree/blob/main/LICENSE.md">
    <img src="https://img.shields.io/github/license/Analogy-LogViewer/Analogy.LogViewer.LoggersTree" img alt="License"/>
</a>
[![Nuget](https://img.shields.io/nuget/v/Analogy.LogViewer.LoggersTree)](https://www.nuget.org/packages/Analogy.LogViewer.LoggersTree/)
[![Nuget](https://img.shields.io/nuget/dt/Analogy.LogViewer.LoggersTree)](https://www.nuget.org/packages/Analogy.LogViewer.LoggersTree/)


Display a tree of of available loggers in corresponding log window, each logger can be individually filtered by level.

![Screenshot](screenshot.png)

## Tree structure

- A new root is created for each unique pair of MachineName:Module
- Each root will show the tree of the different sources (loggers) available
- Each leaf display the current level filter as an Icon

## Filtering

Any leaf of the tree can have a filter applied, which will also be done for the children.
Filtering is done on the log level, via the context menu.

![Context menu](screenshot-menu.png)

When the filtering is modified, a new SQL query would be created, which is shown at the bottom.
The query is then automatically applied on the corresponding log window.

---
**Important**

Advanced feature *Raw SQL filtering* should be enabled.

---
