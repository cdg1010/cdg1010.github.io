# Using the Hub from the command line

## Use Unity Hub from the command line

The latest version of the Unity Hub offers an alternative command line interface.

Before you can use these commands, [download and install the Hub.]

## Display help

From your command line, navigate to the location of _Unity Hub.exe _and run the command `--headless help. `

### Examples

macOS:

```
Unity\ Hub.app/Contents/MacOS/Unity\ Hub --headless help
```

Windows:

```
C:\Program Files\Unity Hub>"Unity Hub.exe" --headless help
```

## Manage editors

The command `editors` (or `e`) shows a combined list of the available releases and installed editors on your machine.

To reduce the scope of the list, specify one of the following options:

<table>
  <tr>
   <td><strong>Option</strong>
   </td>
   <td><strong>Alias</strong>
   </td>
   <td><strong>Description</strong>
   </td>
  </tr>
  <tr>
   <td><code>--releases</code>
   </td>
   <td><code>-r </code>
   </td>
   <td>Shows only the available releases.
   </td>
  </tr>
  <tr>
   <td><code>--installed</code>
   </td>
   <td><code>-i</code>
   </td>
   <td>Shows only the installed editors on your machine.
   </td>
  </tr>
</table>

### Examples

macOS:

```
Unity\ Hub.app/Contents/MacOS/Unity\ Hub -- --headless editors -r
```

Windows:

```
C:\Program Files\Unity Hub>"Unity Hub.exe" -- --headless editors -r
```


## Set install path

The command install-path (alias `ip`) set/get the path where the Unity editors will be installed

This command supports the following options:

<table>
  <tr>
   <td><strong>Option</strong>
   </td>
   <td><strong>Alias</strong>
   </td>
   <td><strong>Description</strong>
   </td>
  </tr>
  <tr>
   <td><code>--set &lt;path></code>
   </td>
   <td><code>-s </code>
   </td>
   <td>Shows only the available releases.
   </td>
  </tr>
  <tr>
   <td><code>--get</code>
   </td>
   <td><code>-g</code>
   </td>
   <td>Shows only the installed editors on your machine
   </td>
  </tr>
</table>

### Examples

macOS:

```
Unity\ Hub.app/Contents/MacOS/Unity\ Hub -- --headless install-path
-s /Applications/Unity/Hub/Editor/
```

Windows:

```
C:\Program Files\Unity Hub>"Unity Hub.exe" -- --headless install-path
-s /Applications/Unity/Hub/Editor/
```

## Install Editor versions

<code>install<strong> </strong></code>(or<code> i) </code>installs a new editor either from the releases list or archive

This command supports the following options:

<table>
  <tr>
   <td><strong>Option</strong>
   </td>
   <td><strong>Alias</strong>
   </td>
   <td><strong>Description</strong>
   </td>
  </tr>
  <tr>
   <td><code>--version  &lt;version></code>
   </td>
   <td><code>-v</code>
   </td>
   <td>The Editor version to be installed (e.g. 2019.1.11f1).
<p>
<em>(Required.)</em>
   </td>
  </tr>
  <tr>
   <td><code>--changeset  &lt;changeset></code>
   </td>
   <td><code>-c</code>
   </td>
   <td>The changeset of the Editor if it is not in the release list (e.g. 9b001d489a54).
<p>
<em>(Required if the version is not in the list of releases: consult the Release Notes or run <code>editors -r</code> to check.)</em>
   </td>
  </tr>
  <tr>
   <td><code>--module &lt;moduleid></code>
   </td>
   <td><code>-m</code>
   </td>
   <td>The module ID. See install-modules for more information.
   </td>
  </tr>
</table>

### Examples

macOS:

```
Unity\ Hub.app/Contents/MacOS/Unity\ Hub -- --headless install --v 2019.1.11f1 --changeset 9b001d489a54
```

Windows:

```
C:\Program Files\Unity Hub>"Unity Hub.exe" -- --headless install --v 2019.1.11f1 --changeset 9b001d489a54
```

## Install modules {#install-modules}

**<code>install-modules </code></strong>(or<strong><code> im</code></strong>)<strong><code> </code></strong>lets you<strong><code> </code></strong>download and install a module (e.g. build support) to an installed Editor.

This** **command supports the following options:


<table>
  <tr>
   <td><strong><code>--version|-v &lt;version> </code></strong>
   </td>
   <td>Specifies the version of the Editor to add the module to. <em>(Required)</em>
   </td>
  </tr>
  <tr>
   <td><strong><code>--module|-m  &lt;moduleid></code></strong>
   </td>
   <td>Specifies the module ID. You can add more than one module.
<p>
For the available modules, see the table below.
<p>
**Note:** Not all modules are available for every version of the Editor.
   </td>
  </tr>
</table>

### Available modules

<table>
  <tr>
   <td><strong>Module</strong>
   </td>
   <td><strong>Value</strong>
   </td>
  </tr>
  <tr>
   <td>Documentation
   </td>
   <td><strong><code>documentation</code></strong>
   </td>
  </tr>
  <tr>
   <td>Standard assets
   </td>
   <td><strong><code>standardassets</code></strong>
   </td>
  </tr>
  <tr>
   <td>Example Project
   </td>
   <td><strong><code>example</code></strong>
   </td>
  </tr>
  <tr>
   <td>Android build support
   </td>
   <td><strong><code>android</code></strong>
   </td>
  </tr>
  <tr>
   <td>iOS build support
   </td>
   <td><strong><code>ios</code></strong>
   </td>
  </tr>
  <tr>
   <td>tvOS build support
   </td>
   <td><strong><code>appletv</code></strong>
   </td>
  </tr>
  <tr>
   <td>Linux build support
   </td>
   <td><strong><code>linux</code></strong>
   </td>
  </tr>
  <tr>
   <td>Samsung TV build support
   </td>
   <td><strong><code>samsung</code></strong>
   </td>
  </tr>
  <tr>
   <td>Tizen build support
   </td>
   <td><strong><code>tizen</code></strong>
   </td>
  </tr>
  <tr>
   <td>WebGL build support
   </td>
   <td><strong><code>webgl</code></strong>
   </td>
  </tr>
  <tr>
   <td>Windows build support
   </td>
   <td><strong><code>windows</code></strong>
   </td>
  </tr>
  <tr>
   <td>Facebook Gameroom build support
   </td>
   <td><strong><code>facebook-games</code></strong>
   </td>
  </tr>
  <tr>
   <td>MonoDevelop/Unity Debugger
   </td>
   <td><strong><code>monodevelop</code></strong>
   </td>
  </tr>
  <tr>
   <td>Vuforia Augmented Reality support
   </td>
   <td><strong><code>vuforia-ar</code></strong>
   </td>
  </tr>
  <tr>
   <td>Language packs
   </td>
   <td><strong><code>language-ja,  \
language-ko,  \
language-zh-cn, language-zh-hant, language-zh-hans</code></strong>
   </td>
  </tr>
  <tr>
   <td>Mac build support (IL2CPP)
   </td>
   <td><strong><code>mac-il2cpp</code></strong>
   </td>
  </tr>
  <tr>
   <td>Windows build support (Mono)
   </td>
   <td><strong><code>windows-mono</code></strong>
   </td>
  </tr>
  <tr>
   <td>Android SDK & NDK tools
   </td>
   <td><strong><code>android-sdk-ndk-tools</code></strong>
   </td>
  </tr>
  <tr>
   <td>Lumin OS (Magic Leap) build support
   </td>
   <td><strong><code>lumin</code></strong>
   </td>
  </tr>
</table>

### Examples

macOS:

```
Unity\ Hub.app/Contents/MacOS/Unity\ Hub -- --headless install-modules --version 2019.1.11f1 -m ios -m android
```

Windows:

```
C:\Program Files\Unity Hub>"Unity Hub.exe" -- --headless install-modules --version 2019.1.11f1 -m ios -m android
```
