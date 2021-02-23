<!DOCTYPE html>
<!--[if IE]><![endif]-->
<html>
  
  <head>
  
  <meta name="robots" content="noindex">
  
    <script>(function(w,d,s,l,i){w[l]=w[l]||[];w[l].push({'gtm.start':  new Date().getTime(),event:'gtm.js'});var f=d.getElementsByTagName(s)[0],   j=d.createElement(s),dl=l!='dataLayer'?'&l='+l:'';j.async=true;j.src=   'https://www.googletagmanager.com/gtm.js?id='+i+dl;f.parentNode.insertBefore(j,f);  })(window,document,'script','dataLayer','GTM-5V25JL6');</script>
  
  
  <script>
  $(document).ready(function(){
    $('#component-select-current-display').click(function(){
        $('#component-select-current-display').toggleClass('component-select__current--is-active');
    });
    $(document).click(function(e){
        if (!(e.target.id == 'component-select-current-display'))
            $('#component-select-current-display').removeClass('component-select__current--is-active');
    });
     });
  </script>
  
    <meta charset="utf-8">
    <meta http-equiv="X-UA-Compatible" content="IE=edge,chrome=1">
  
    <meta http-equiv="Cache-Control" content="no-cache, no-store, must-revalidate">
    <meta http-equiv="Pragma" content="no-cache">
    <meta http-equiv="Expires" content="0">
  
    <title>Adding a new radial dial widget | Unity Reflect | 2.0 </title>
    <meta name="viewport" content="width=device-width">
    <meta name="title" content="Adding a new radial dial widget | Unity Reflect | 2.0 ">
    <meta name="generator" content="docfx 2.45.0.0">
    
    <link rel="shortcut icon" href="../../favicon.ico">
    <link rel="stylesheet" href="../../styles/docfx.vendor.css">
    <link rel="stylesheet" href="../../styles/docfx.css">
    <link rel="stylesheet" href="../../styles/main.css">
    <link rel="stylesheet" href="../../styles/version-switcher.css">
    <meta property="docfx:navrel" content="../../toc.html">
    <meta property="docfx:tocrel" content="../../toc.html">
    <meta property="unity:packageTitle" content="Unity Reflect | 2.0">
    <meta property="docfx:rel" content="../../">
    
  
  </head>
  <body data-spy="scroll" data-target="#affix" class="manual">
	<noscript><iframe src="https://www.googletagmanager.com/ns.html?id=GTM-5V25JL6" height="0" width="0" style="display:none;visibility:hidden"></iframe></noscript>
    <div id="wrapper">
      <header>
        
        <nav id="autocollapse" class="navbar navbar-inverse ng-scope" role="navigation">
          <div class="container">
            <div class="navbar-header">
              <button type="button" class="navbar-toggle" data-toggle="collapse" data-target="#navbar">
                <span class="sr-only">Toggle navigation</span>
                <span class="icon-bar"></span>
                <span class="icon-bar"></span>
                <span class="icon-bar"></span>
              </button>
              
              <a class="navbar-brand" href="../../index.html">
                <img id="logo" class="svg" src="../../logo.svg" alt="">
              </a>
            </div>
            <div class="collapse navbar-collapse" id="navbar">
              <form class="navbar-form navbar-right" role="search" id="search">
                <div class="form-group">
                  <input type="text" class="form-control" id="search-query" placeholder="Search" autocomplete="off">
                </div>
        
        		<div class="back-to-unity-group">
        			<a class="back-to-unity" href="http://docs.unity3d.com/">docs.unity3d.com</a>
        		</div>
        
              </form>
            </div>
          </div>
        </nav>
        <div class="subnav navbar navbar-default">
        <div class="container hide-when-search" id="breadcrumb">
          <div id="version-switcher-select">
        <div class="component-select" onclick="onVersionSwitcherClick()">
        <div id="component-select-current-display" class="component-select__current">
        Unity Reflect 2.0
        </div>
        
        <ul id="version-switcher-ul" class="component-select__options-container">
        <a style="color:#000;" href="https://docs.unity3d.com/reflect/1.3/manual/index.html"><li class="component-select__option">
                    1.3
                </li></a>
        <a style="color:#000;" href="https://docs.unity3d.com/reflect/1.2/manual/index.html"><li class="component-select__option">
                    1.2
                </li></a>
        <a style="color:#000;" href="https://docs.unity3d.com/reflect/1.1/manual/index.html"><li class="component-select__option">
                    1.1
                </li></a>
                <a style="color:#000;" href="https://docs.unity3d.com/reflect/1.0/manual/index.html"><li class="component-select__option">
                            1.0
                        </li></a>
        
        
                </ul>
        </div>
        </div></div>
      </div></header>
      <div class="container body-content">
        
        <div id="search-results">
          <div class="search-list"></div>
          <div class="sr-items"></div>
          <ul id="pagination"></ul>
        </div>
      </div>
      <div role="main" class="container body-content hide-when-search">
        <div class="article row grid">
          <div class="col-md-10" id="main_content">
          <div id="breadcrumb_placeholder"></div>
            <article class="content wrap" id="_content" data-uid="">
<h1 id="adding-a-new-radial-dial-widget" sourcefile="manual/devguide/NewRadial.md" sourcestartlinenumber="1" sourceendlinenumber="1">Adding a new radial dial widget</h1>

<p sourcefile="manual/devguide/NewRadial.md" sourcestartlinenumber="3" sourceendlinenumber="3">This walkthrough shows you how to add a new radial widget and respond to a user’s changes in the viewer.</p>
<p sourcefile="manual/devguide/NewRadial.md" sourcestartlinenumber="5" sourceendlinenumber="5">The example we use adds a dial to set the time of day and time of year with the Sun Study feature.</p>
<h2 id="model-view-controller" sourcefile="manual/devguide/NewRadial.md" sourcestartlinenumber="7" sourceendlinenumber="7">Model, View, Controller</h2>
<p sourcefile="manual/devguide/NewRadial.md" sourcestartlinenumber="9" sourceendlinenumber="9">Reflect Viewer uses the unity.touch-framework package to implement the UI, based on centralized MVC.</p>
<p sourcefile="manual/devguide/NewRadial.md" sourcestartlinenumber="11" sourceendlinenumber="11"><img src="../images/1.3/Radial1.png" alt="Radial" sourcefile="manual/devguide/NewRadial.md" sourcestartlinenumber="11" sourceendlinenumber="11"></p>
<p sourcefile="manual/devguide/NewRadial.md" sourcestartlinenumber="13" sourceendlinenumber="13">We use UGUI as our View framework, MonoBehaviours as the Controller basis, and we have a centralized Model using UIDATA structs. We are also using <a href="https://facebook.github.io/flux/" data-raw-source="[Flux](https://facebook.github.io/flux/)" sourcefile="manual/devguide/NewRadial.md" sourcestartlinenumber="13" sourceendlinenumber="13">Flux</a> C# implementation <a href="https://github.com/samih7/SharpFlux" data-raw-source="[SharpFlux](https://github.com/samih7/SharpFlux)" sourcefile="manual/devguide/NewRadial.md" sourcestartlinenumber="13" sourceendlinenumber="13">SharpFlux</a>  to manipulate the Model.</p>
<h3 id="model" sourcefile="manual/devguide/NewRadial.md" sourcestartlinenumber="15" sourceendlinenumber="15">Model</h3>
<h4 id="create-new-ui-data-model" sourcefile="manual/devguide/NewRadial.md" sourcestartlinenumber="17" sourceendlinenumber="17">Create new UI Data Model</h4>
<p sourcefile="manual/devguide/NewRadial.md" sourcestartlinenumber="19" sourceendlinenumber="19">If necessary, create a new UI Data Struct and add to UIStateData, UISessionStateData or UIProjectStateData depending on the data usage. In the example, the TimeOfDayYearDial uses the already-existing SunStudyData, so nothing new was created.</p>
<h3 id="view" sourcefile="manual/devguide/NewRadial.md" sourcestartlinenumber="21" sourceendlinenumber="21">View</h3>
<p sourcefile="manual/devguide/NewRadial.md" sourcestartlinenumber="23" sourceendlinenumber="23">We need to create the outer month dial and inner hour dial.</p>
<h4 id="create-a-new-radial-prefab" sourcefile="manual/devguide/NewRadial.md" sourcestartlinenumber="25" sourceendlinenumber="25">Create a New Radial Prefab</h4>
<p sourcefile="manual/devguide/NewRadial.md" sourcestartlinenumber="27" sourceendlinenumber="27">Create the prefab and add all the necessary UI controls using the Dial Control prefab from the unity.touch-framework package. The Time of Day Year Radial prefab includes two Dial Control prefabs, one for each of the inner and outer dials, along with three buttons.</p>
<p sourcefile="manual/devguide/NewRadial.md" sourcestartlinenumber="29" sourceendlinenumber="29"><img src="../images/1.3/Radial2.png" alt="Radial" sourcefile="manual/devguide/NewRadial.md" sourcestartlinenumber="29" sourceendlinenumber="29"></p>
<p sourcefile="manual/devguide/NewRadial.md" sourcestartlinenumber="31" sourceendlinenumber="31"><img src="../images/1.3/Radial3.png" alt="Radial" sourcefile="manual/devguide/NewRadial.md" sourcestartlinenumber="31" sourceendlinenumber="31"></p>
<p sourcefile="manual/devguide/NewRadial.md" sourcestartlinenumber="33" sourceendlinenumber="33">To create similar radial dials, you can copy and paste the prefabs in the “Assets/Prefabs/UI/Controls/Radial Dials” folder, perhaps as a prefab variant. Place your prefab in this folder as well.</p>
<h3 id="controller" sourcefile="manual/devguide/NewRadial.md" sourcestartlinenumber="35" sourceendlinenumber="35">Controller</h3>
<p sourcefile="manual/devguide/NewRadial.md" sourcestartlinenumber="37" sourceendlinenumber="37">Since the Time of Day Year Radial completely replaces the right-side toolbars (such as the Orbit Sidebar) when opened, it was added as a new ToolbarType and controlled via ActiveToolbarController.cs.</p>
<blockquote sourcefile="manual/devguide/NewRadial.md" sourcestartlinenumber="39" sourceendlinenumber="39"><p sourcefile="manual/devguide/NewRadial.md" sourcestartlinenumber="39" sourceendlinenumber="39"><strong>Note:</strong> This may not be applicable to other radial widgets, particularly those that do not replace other toolbars.</p>
</blockquote>
<h4 id="add-your-new-toolbartype" sourcefile="manual/devguide/NewRadial.md" sourcestartlinenumber="41" sourceendlinenumber="41">Add your new ToolbarType</h4>
<p sourcefile="manual/devguide/NewRadial.md" sourcestartlinenumber="43" sourceendlinenumber="43">Add a new ToolbarType in UIStateData as shown below:</p>
<p sourcefile="manual/devguide/NewRadial.md" sourcestartlinenumber="45" sourceendlinenumber="45"><img src="../images/1.3/Radial5.png" alt="Radial" sourcefile="manual/devguide/NewRadial.md" sourcestartlinenumber="45" sourceendlinenumber="45"></p>
<p sourcefile="manual/devguide/NewRadial.md" sourcestartlinenumber="47" sourceendlinenumber="47"><em>Path: Assets/Scripts/UI/UIStateData.cs</em></p>
<p sourcefile="manual/devguide/NewRadial.md" sourcestartlinenumber="49" sourceendlinenumber="49">UIStateData is used in UIStateManager.cs, allowing centralized access to the active toolbar.</p>
<p sourcefile="manual/devguide/NewRadial.md" sourcestartlinenumber="51" sourceendlinenumber="51"><img src="../images/1.3/Radial6.png" alt="Radial" sourcefile="manual/devguide/NewRadial.md" sourcestartlinenumber="51" sourceendlinenumber="51"></p>
<p sourcefile="manual/devguide/NewRadial.md" sourcestartlinenumber="53" sourceendlinenumber="53"><em>Path: Assets/Scripts/UI/UIStateData.cs</em></p>
<h4 id="add-your-new-radial-to-activetoolbarcontroller" sourcefile="manual/devguide/NewRadial.md" sourcestartlinenumber="55" sourceendlinenumber="55">Add Your New Radial to ActiveToolbarController</h4>
<p sourcefile="manual/devguide/NewRadial.md" sourcestartlinenumber="57" sourceendlinenumber="57">Open ActiveToolbarController.cs and add your new dial with the following steps:</p>
<ol sourcefile="manual/devguide/NewRadial.md" sourcestartlinenumber="59" sourceendlinenumber="69">
<li sourcefile="manual/devguide/NewRadial.md" sourcestartlinenumber="59" sourceendlinenumber="61"><p sourcefile="manual/devguide/NewRadial.md" sourcestartlinenumber="59" sourceendlinenumber="59">Add a new member for the new dial.</p>
<p sourcefile="manual/devguide/NewRadial.md" sourcestartlinenumber="61" sourceendlinenumber="61"><img src="../images/1.3/Radial7.png" alt="Radial" sourcefile="manual/devguide/NewRadial.md" sourcestartlinenumber="61" sourceendlinenumber="61"></p>
</li>
<li sourcefile="manual/devguide/NewRadial.md" sourcestartlinenumber="63" sourceendlinenumber="65"><p sourcefile="manual/devguide/NewRadial.md" sourcestartlinenumber="63" sourceendlinenumber="63">Update the OnStateDataChanged() event handler.</p>
<p sourcefile="manual/devguide/NewRadial.md" sourcestartlinenumber="65" sourceendlinenumber="65"><img src="../images/1.3/Radial8.png" alt="Radial" sourcefile="manual/devguide/NewRadial.md" sourcestartlinenumber="65" sourceendlinenumber="65"></p>
</li>
<li sourcefile="manual/devguide/NewRadial.md" sourcestartlinenumber="67" sourceendlinenumber="69"><p sourcefile="manual/devguide/NewRadial.md" sourcestartlinenumber="67" sourceendlinenumber="67">Add your switch case for your DialogType.</p>
<p sourcefile="manual/devguide/NewRadial.md" sourcestartlinenumber="69" sourceendlinenumber="69"><img src="../images/1.3/Radial9.png" alt="Radial" sourcefile="manual/devguide/NewRadial.md" sourcestartlinenumber="69" sourceendlinenumber="69"></p>
</li>
</ol>
<p sourcefile="manual/devguide/NewRadial.md" sourcestartlinenumber="71" sourceendlinenumber="71">Now you can create the controller class to handle all the changes and data modifications.</p>
<h4 id="create-the-timeradialuicontrollercs-class" sourcefile="manual/devguide/NewRadial.md" sourcestartlinenumber="73" sourceendlinenumber="73">Create the TimeRadialUIController.cs class.</h4>
<p sourcefile="manual/devguide/NewRadial.md" sourcestartlinenumber="75" sourceendlinenumber="75">Every Radial prefab has a controller. Controllers are used to respond to UIData changes and to support UI user events (button presses, sliders, etc.). Do not hesitate to duplicate an existing radial controller (named ___RadialUIController.cs) and rename it. The AltitudeAzimuthRadialUIController.cs or ARScaleRadialUIController.cs scripts are recommended for copying because they are simpler than TimeRadialController.cs.</p>
<p sourcefile="manual/devguide/NewRadial.md" sourcestartlinenumber="77" sourceendlinenumber="77"><img src="../images/1.3/Radial10.png" alt="Radial" sourcefile="manual/devguide/NewRadial.md" sourcestartlinenumber="77" sourceendlinenumber="77"></p>
<p sourcefile="manual/devguide/NewRadial.md" sourcestartlinenumber="79" sourceendlinenumber="79">Add member controls for the UI with [SerializeField]. Also add local private members like previousToolbar to display the most recent toolbar on closing the dial. See the code below OnStateDataChanged():</p>
<pre sourcefile="manual/devguide/NewRadial.md" sourcestartlinenumber="81" sourceendlinenumber="111"><code>using System;
using SharpFlux;
Using Unity.TouchFramework;
using UnityEngine;
using UnityEngine.UI;

namespace Unity.Reflect.Viewer.UI
{
  public class TimeRadialUIController : MonoBehaviour
   {
#pragma warning disable CS0649
       [SerializeField]
       DialControl m_HourDialControl;
       [SerializeField]
       DialControl m_MonthDialControl;
       [SerializeField]
       Button m_MainButton;
       [SerializeField]
       Button m_SecondaryButton;
       [SerializeField]
       Button m_ResetButton;
#pragma warning restore CS0649

       int m_DefaultHour;
       int m_DefaultMonth;
       public static ToolbarType m_previousToolbar; // display previous toolbar on dial closed
       readonly MonthLabelConverter m_MonthLabels = new MonthLabelConverter();
       readonly HourLabelConverter m_HourLabels = new HourLabelConverter();
</code></pre><p sourcefile="manual/devguide/NewRadial.md" sourcestartlinenumber="113" sourceendlinenumber="113">Add Listener to UI controls in Start().</p>
<pre sourcefile="manual/devguide/NewRadial.md" sourcestartlinenumber="115" sourceendlinenumber="139"><code>void Awake()
{
     UIStateManager.stateChanged += OnStateDataChanged;
      m_MonthDialControl.labelConverter = m_MonthLabels;
       m_HourDialControl.labelConverter = m_HourLabels;
}

void Start()
{
     m_HourDialControl.onSelectedValueChanged.AddListener(OnHourDialValueChanged);
     m_MonthDialControl.onSelectedValueChanged.AddListener(OnMonthDialValueChanged);
     m_ResetButton.onClick.AddListener(OnResetButtonClicked);

     m_MainButton.onClick.AddListener(OnMainButtonClicked);
     m_SecondaryButton.onClick.AddListener(OnSecondaryButtonClicked);

     int min, day;
     (m_DefaultHour, min) = SunStudyUIController.GetHourMinute(UIStateManager.current.stateData.sunStudyData.timeOfDay);
     (m_DefaultMonth, day) = SunStudyUIController.GetMonthDay(DateTime.Now.Year, UIStateManager.current.stateData.sunStudyData.timeOfYear);
     m_HourDialControl.selectedValue = m_DefaultHour;
      m_MonthDialControl.selectedValue = m_DefaultMonth;
}
</code></pre><p sourcefile="manual/devguide/NewRadial.md" sourcestartlinenumber="141" sourceendlinenumber="141">OnStateDataChanged, remember to set the Dials’ selectedValue. This moves the dials whenever the appropriate state data is changed, such as sunStudyData.timeOfDay via the Sun Study Dialog time sliders. OnMonthDialValueChanged changes the Sun Study Data via dispatching the SetSunStudy action.</p>
<pre sourcefile="manual/devguide/NewRadial.md" sourcestartlinenumber="143" sourceendlinenumber="167"><code>void OnStateDataChanged(UIStateData data)
        {
            m_HourDialControl.selectedValue = GetFloatFromMin(data.sunStudyData.timeOfDay);
            m_MonthDialControl.selectedValue = GetFloatFromDay(data.sunStudyData.timeOfYear);

            if (data.activeToolbar != ToolbarType.TimeOfDayYearDial &amp;&amp; data.activeToolbar != ToolbarType.AltitudeAzimuthDial)
            {
                m_previousToolbar = data.activeToolbar;
            }
        }

        void OnMonthDialValueChanged(float value)
        {
            var data = UIStateManager.current.stateData.sunStudyData;
            data.timeOfYear = GetDayFromFloat(value);
            UIStateManager.current.Dispatcher.Dispatch(Payload&lt;ActionTypes&gt;.From(ActionTypes.SetSunStudy, data));
        }


// … code not shown, see full script for reference

   }
}
</code></pre><p sourcefile="manual/devguide/NewRadial.md" sourcestartlinenumber="168" sourceendlinenumber="168"><em>Path: Assets/Scripts/UI/Controllers/TimeRadialUIController.cs</em></p>
<p sourcefile="manual/devguide/NewRadial.md" sourcestartlinenumber="170" sourceendlinenumber="170">At this stage, you can tie everything together.</p>
<h2 id="add-the-timeradialuicontroller-component-to-the-prefab" sourcefile="manual/devguide/NewRadial.md" sourcestartlinenumber="172" sourceendlinenumber="172">Add the TimeRadialUIController Component to the Prefab</h2>
<p sourcefile="manual/devguide/NewRadial.md" sourcestartlinenumber="174" sourceendlinenumber="175">Edit the prefab (enter nested prefab edition mode). In the Inspector, add the component.
If you created your Radial prefab from scratch instead of copying an existing prefab, make sure you have all required components (Canvas, Canvas Group, Graphic Raycaster, and Dialog Window).</p>
<p sourcefile="manual/devguide/NewRadial.md" sourcestartlinenumber="177" sourceendlinenumber="177"><img src="../images/1.3/Radial11.png" alt="Radial" sourcefile="manual/devguide/NewRadial.md" sourcestartlinenumber="177" sourceendlinenumber="177"></p>
<h2 id="put-your-prefab-in-the-scene" sourcefile="manual/devguide/NewRadial.md" sourcestartlinenumber="179" sourceendlinenumber="179">Put your Prefab In the Scene</h2>
<p sourcefile="manual/devguide/NewRadial.md" sourcestartlinenumber="181" sourceendlinenumber="181">Place your Options Dialog under UI Main in the Reflect scene next to other sidebar prefabs.</p>
<p sourcefile="manual/devguide/NewRadial.md" sourcestartlinenumber="183" sourceendlinenumber="183"><img src="../images/1.3/Radial12.png" alt="Radial" sourcefile="manual/devguide/NewRadial.md" sourcestartlinenumber="183" sourceendlinenumber="183"></p>
<p sourcefile="manual/devguide/NewRadial.md" sourcestartlinenumber="185" sourceendlinenumber="185">Select “UI Root” GameObject. There is an Active Toolbar Controller in the Inspector window. Drag and drop the Time of Day Year Radial Variant to the Inspector window to set the value.</p>
<p sourcefile="manual/devguide/NewRadial.md" sourcestartlinenumber="187" sourceendlinenumber="187"><img src="../images/1.3/Radial13.png" alt="Radial" sourcefile="manual/devguide/NewRadial.md" sourcestartlinenumber="187" sourceendlinenumber="187"></p>
<p sourcefile="manual/devguide/NewRadial.md" sourcestartlinenumber="189" sourceendlinenumber="189">Disable the Canvas component to hide the radial by default. Visibility is controlled by DialogWindow component and the ActiveToolbarController.</p>
<p sourcefile="manual/devguide/NewRadial.md" sourcestartlinenumber="191" sourceendlinenumber="191"><img src="../images/1.3/Radial14.png" alt="Radial" sourcefile="manual/devguide/NewRadial.md" sourcestartlinenumber="191" sourceendlinenumber="191"></p>
<h4 id="create-the-button-in-the-toolbar-and-link-to-the-radial" sourcefile="manual/devguide/NewRadial.md" sourcestartlinenumber="193" sourceendlinenumber="193">Create the button in the toolbar and link to the radial</h4>
<p sourcefile="manual/devguide/NewRadial.md" sourcestartlinenumber="195" sourceendlinenumber="195">The Time Dial is opened via the Sun Study button on the right toolbar (Orbit Sidebar). Make sure to create the button on the Orbit Sidebar prefab and add in necessary code to RightSidebarController.cs.</p>
<p sourcefile="manual/devguide/NewRadial.md" sourcestartlinenumber="197" sourceendlinenumber="197"><img src="../images/1.3/Radial15.png" alt="Radial" sourcefile="manual/devguide/NewRadial.md" sourcestartlinenumber="197" sourceendlinenumber="197"></p>
<p sourcefile="manual/devguide/NewRadial.md" sourcestartlinenumber="199" sourceendlinenumber="199">The dial opens when the Sun Study button is clicked.</p>
<h2 id="compile-and-run" sourcefile="manual/devguide/NewRadial.md" sourcestartlinenumber="201" sourceendlinenumber="201">Compile and Run</h2>
<p sourcefile="manual/devguide/NewRadial.md" sourcestartlinenumber="203" sourceendlinenumber="203">Compile and run. Check to make sure that the new dial opens when you click it. If so, keep going to implement the rest of your UI controller.</p>
<p sourcefile="manual/devguide/NewRadial.md" sourcestartlinenumber="205" sourceendlinenumber="205"><img src="../images/1.3/Radial22.gif" alt="Radial" sourcefile="manual/devguide/NewRadial.md" sourcestartlinenumber="205" sourceendlinenumber="205"></p>
<p sourcefile="manual/devguide/NewRadial.md" sourcestartlinenumber="207" sourceendlinenumber="207">Congratulations, you just added your own custom dial to the Unity Reflect Viewer!</p>
</article>
          </div>
          
          <div class="hidden-sm col-md-2" role="complementary">
            <div class="sideaffix">
              <div class="contribution">
                <ul class="nav">
                </ul>
              </div>
              <nav class="bs-docs-sidebar hidden-print hidden-xs hidden-sm affix" id="affix">
              <!-- <p><a class="back-to-top" href="#top">Back to top</a><p> -->
              </nav>
            </div>
          </div>
        </div>
      </div>
      
      <footer>
        <div class="grad-bottom"></div>
        <div class="footer">
          <div class="container">
            <span class="pull-right">
              <a href="#top">Back to top</a>
            </span>
            
            <span>Copyright © 2020 Unity Technologies<br>Generated by <strong>DocFX</strong></span>
          </div>
        </div>
      </footer>
    
    
    <script type="text/javascript" src="../../styles/docfx.vendor.js"></script>
    <script type="text/javascript" src="../../styles/docfx.js"></script>
    <script type="text/javascript" src="../../styles/main.js"></script>
    <script type="text/javascript" src="../../styles/metadata-collector.js"></script>
    <script type="text/javascript" src="../../styles/version-switcher.js"></script>
  </div></body>
</html>
