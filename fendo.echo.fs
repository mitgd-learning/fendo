.( fendo.echo.fs ) cr

\ This file is part of Fendo.

\ This file defines the words that print to the target HTML file.

\ Copyright (C) 2013,2014 Marcos Cruz (programandala.net)

\ Fendo is free software; you can redistribute
\ it and/or modify it under the terms of the GNU General
\ Public License as published by the Free Software
\ Foundation; either version 2 of the License, or (at your
\ option) any later version.

\ Fendo is distributed in the hope that it will be useful,
\ but WITHOUT ANY WARRANTY; without even the implied
\ warranty of MERCHANTABILITY or FITNESS FOR A PARTICULAR
\ PURPOSE.  See the GNU General Public License for more
\ details.

\ You should have received a copy of the GNU General Public
\ License along with this program; if not, see
\ <http://gnu.org/licenses>.

\ **************************************************************
\ Change history of this file

\ See at the end of the file.

\ **************************************************************
\ Requirements

require galope/number-to-string.fs  \ 'n>str'

\ **************************************************************
\ Output

variable echo>  \ destination of the output
\ Possible values of 'echo>':
0 constant >screen
1 constant >file
2 constant >string

variable echoed  \ used as dynamic string

: echo>string  ( -- )
  \ Redirect the output to the dynamic string 'echoed'.
  >string echo> !  0 echoed $!len
  ;
: echo>file  ( -- )
  \ Redirect the output to the target file.
  >file echo> !
  ;
: echo>screen  ( -- )
  \ Redirect the output to the screen.
  >screen echo> !
  ;

: echo>file?  ( -- wf )
  echo> @ >file =
  ;
: echo>screen?  ( -- wf )
  echo> @ >screen =
  ;
: echo>string?  ( -- wf )
  echo> @ >string =
  ;

echo>file

\ **************************************************************
\ Echo

variable target_fid  \ file id of the HTML target page

: (echo)  ( xt | ca len xt -- )
  echo>screen?
  if    execute
  else  target_fid @ outfile-execute
  then
  ;
: (echo>string)  ( ca len -- )
  \ Add a string to the 'echoed' string.
  echoed $+!
  ;
: echo  ( ca len -- )
  \ Print a text string to the HTML file.
\  2dup cr type  key drop  \ xxx informer
  echo>string?
  if  (echo>string)  else  ['] type (echo)  then
  ;
variable separate?  \ flag: separate the next tag or word from the current one?
: echo_cr  ( -- )
  \ Print a carriage return to the HTML file.
  echo>string?
  if    s\" \n"  (echo>string)
  else  ['] cr (echo)
  then  separate? off
  ;
' echo_cr alias \n

: echo_space  ( -- )
  \ Print a space to the HTML file.
  s"  " echo
  ;
: echo_quote  ( -- )
  \ Print a double quote to the HTML file.
  s\" \"" echo
  ;
: echo_period  ( -- )
  \ Print a period to the HTML file.
  s" ." echo
  ;
: _separate  ( -- )
  \ Separate the current tag or word from the previous one, if needed.
  separate? @ if  echo_space  then  separate? on
  ;
: _echo  ( ca len -- )
  \ Print a text string to the HTML file, with a previous space if needed.
  _separate echo
  ;
: ?_echo  ( ca len f -- )  \ xxx used?
  if  _echo  else  2drop  then
  ;
: echo_line  ( ca len -- )
  echo echo_cr
  ;
: ?echo_line  ( ca len f -- )
  if  echo_line  else  2drop  then
  ;
: echo.  ( n -- )
  n>str echo
  ;
: _echo.  ( n -- )
  n>str echo_space echo
  ;
: +echo  ( ca len -- )
  separate? off  echo
  ;

.( fendo.echo.fs compiled) cr

\ **************************************************************
\ Change history of this file

\ 2013-06-04: Start.
\ 2013-06-08: New: First code for output redirection.
\ 2013-06-29: Change: 'target_fid' moved here from <fendo_files.fs>.
\ 2013-07-03: Change: 'dry?' renamed to 'echo>screen?'.
\ 2013-07-03: New: tools to redirect the output to a dynamic
\   string.
\ 2013-07-12: New: '?_echo' moved here from <fendo_markup_wiki.fs>.
\ 2013-07-14: New: '?echo_line'.
\ 2013-07-21: New: 'echo.'. This somehow fixes the print corruption
\   caused by using 's>d <# #s #> echo' in the HTML template.
\ 2013-07-21: New: 'echo_line'.
\ 2013-11-07: New: '_echo.'.
\ 2013-11-26: Change: 'n>str' instead of '(echo.)'.
\ 2013-12-06: Fix: 'echo_cr' now does 'separate? off' in order to
\   remove unnecessary blank spaces.
\ 2014-02-15: New: 'echo_period'.
\ 2014-03-11: New: '+echo', experimental, factored from user macros.
