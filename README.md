# TaskbarPlus_Repetier-Host

<p>Plugin for Repetier Host software.</p>

<p>FEATURE:</p>
<p>- Add icon overlay and progress bar in the windows taskbar, icon: connected-play-upload sdcard, sdcard pause, sdcard play <br />
<img src="https://github.com/BrOncOVu/TaskbarPlus_Repetier-Host/blob/master/image%20preview/icon_overlay_preview.png" width="308" height="41" /></p>
<p>- Ability to add custom jump list link in the taskbar and select the automatic list type ( recent, frequent, none ), is possible to add link only in the task category.<br />
<img src="https://github.com/BrOncOVu/TaskbarPlus_Repetier-Host/blob/master/image%20preview/TaskbarPlus_preview.png" width="789" height="616" /></p>
<p>REQUISITE:</p>
<p>Windows 7 or above</p>
<p>INSTALLATION:</p>
<p>Copy the te file TaskbarPlus.dll and the folder translations\ in the repetier host plugin folder in a folder called &quot;TaskbarPlus&quot;, ex: </p>
<p>C:\Program Files\Repetier-Host\plugins\TaskbarPlus\TaskbarPlus.dll</p>
<p>C:\Program Files\Repetier-Host\plugins\TaskbarPlus\translations\</p>
<p>USAGE:</p>
<p>After starting Repetier Host <span id="result_box" lang="en" xml:lang="en">the plugin is already active</span>, in the first start if you ave <span id="result_box" lang="en" xml:lang="en">previously</span>  pinned repetier-host icon to taskbar is possible they appear two repetier-host icon in the taskbar, just unpin the old icon and pin to taskbar the new icon. </p>
<p>Open Repetier host preference for set the plugin. </p>
<p>&nbsp;</p>
<p>NOTE:</p>
<p>The plugin add a key in the registry windows, if you unistall the plugin the key remain in the registry and the Recent/Frequent list in the taskbar can not work properly.</p>
<p><span id="result_box" lang="en" xml:lang="en">If this is the case, remove this string by hand</span>:</p>
<p>HKEY_CURRENT_USER\Software\Classes\Repetier-Host AppUserModelID</p>
