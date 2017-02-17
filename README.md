# ConfOSK

Make the Windows On-Screen Keyboard more easy to use.

*Currently the Simplified Chinese window title "屏幕键盘" is hardcoded to the `FindWindow` arguments. If you're using another language, please fork and do it yourself.*

## Usage

You can use the Windows Task Scheduler & Event Logs to automatically configure the OSK.  
To enable the process start events logging, see [this answer](http://superuser.com/a/603488).

### Lock window size
`confosk lock {on|off}`

### Change opacity
`confosk opacity {0..255}`
