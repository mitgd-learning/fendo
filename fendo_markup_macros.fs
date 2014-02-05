.( fendo_markup_macros.fs ) cr

\ This file is part of Fendo.

\ This file provides the tools to create the user macros.

\ Copyright (C) 2014 Marcos Cruz (programandala.net)

\ Fendo is free software; you can redistribute it and/or modify it
\ under the terms of the GNU General Public License as published by
\ the Free Software Foundation; either version 2 of the License, or
\ (at your option) any later version.

\ Fendo is distributed in the hope that it will be useful, but WITHOUT
\ ANY WARRANTY; without even the implied warranty of MERCHANTABILITY
\ or FITNESS FOR A PARTICULAR PURPOSE.  See the GNU General Public
\ License for more details.

\ You should have received a copy of the GNU General Public
\ License along with this program; if not, see
\ <http://gnu.org/licenses>.

\ Fendo is written in Forth with Gforth
\ (<http://www.bernd-paysan.de/gforth.html>).

\ **************************************************************
\ Change history of this file

\ 2014-02-05: Start, based on <addons/abbr.fs>.

\ **************************************************************
\ Todo


\ **************************************************************

: macro: ( "name" -- )
  \ Create an user macro.
  get-current >r  fendo_markup_macros_wid set-current  :
  r> set-current
  ;
: macro_alias ( "name" -- )
  \ Create an alias of the latest created user macro.
  get-current  fendo_markup_macros_wid set-current  latestxt alias
  set-current
  ;


