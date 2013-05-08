\ fendo_content.fs

\ This file is part of
\ Fendo ("Forth Engine for Net DOcuments") version A-00.

\ This file defines the page content tools.

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

\ Fendo is written in Forth
\ with Gforth (<http://www.bernd-paysan.de/gforth.html>).

\ **************************************************************
\ Change history of this file

\ 2013-04-28 Start.

\ **************************************************************
\ Requirements

require galope/backslash-end-of-file.fs  \ '\eof'

\ **************************************************************
\ Content marks

variable get_content?  \ flag: get the page content? (otherwise, skip it)
: content{  ( "text }content" -- )
  \ Mark the start of the page content. 
  get_content? @ 0= if  \eof  then
  ;

: }content  ( -- )
  \ Mark the end of the page content. 
  \ xxx todo
  get_content? off
  ;

