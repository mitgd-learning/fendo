.( fendo_echo.fs ) cr

\ This file is part of
\ Fendo ("Forth Engine for Net DOcuments") version A-01.

\ This file defines the words that print to the target HTML file.

\ Copyright (C) 2013 Marcos Cruz (programandala.net)

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

\ 2013-06-04 Start.
\ 2013-06-08 New: First code for output redirection.
\ 2013-06-29 Change: 'target_fid' moved here from <fendo_files.fs>.

\ **************************************************************
\ Output

variable dry?  \ flag, dry run?: don't create the target files, echo to the screen instead
dry? off

true value redirection?  \ xxx debug

\ **************************************************************
\ Echo

variable target_fid  \ file id of the HTML target page

: (echo)  ( xt -- )
  dry? @
  if  execute  else  target_fid @ outfile-execute  then
  ;

: echo  ( ca len -- )
  \ Print a text string to the HTML file.
  [ redirection? ] [if]
    ['] type (echo)
  [else]
    type
  [then]
  ;
: echo_cr  ( -- )
  \ Print a carriage return to the HTML file.
  [ redirection? ] [if]
    ['] cr (echo)
  [else]
    cr
  [then]
  ;
: echo_space  ( -- )
  \ Print a space to the HTML file.
  s"  " echo
  ;
: echo_quote  ( -- )
  \ Print a double quote to the HTML file.
  s\" \"" echo 
  ;
variable separate?  \ flag: separate the next tag or word from the current one?
: _separate  ( -- )
  \ Separate the current tag or word from the previous one, if needed.
  separate? @ if  echo_space  then  separate? on
  ;
: _echo  ( ca len -- ) 
  \ Print a text string to the HTML file, with a previous space if needed.
  _separate echo 
  ;

.( fendo_echo.fs compiled) cr


