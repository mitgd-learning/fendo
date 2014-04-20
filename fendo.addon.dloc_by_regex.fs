.( fendo.addon.dloc_by_regex.fs) cr

\ This file is part of Fendo.

\ This file is the code common to several content lists addons.

\ Copyright (C) 2013,2014 Marcos Cruz (programandala.net)

\ Fendo is free software; you can redistribute it and/or modify it
\ under the terms of the GNU General Public License as published by
\ the Free Software Foundation; either version 2 of the License, or
\ (at your option) any later version.

\ Fendo is distributed in the hope that it will be useful, but WITHOUT
\ ANY WARRANTY; without even the implied warranty of MERCHANTABILITY
\ or FITNESS FOR A PARTICULAR PURPOSE.  See the GNU General Public
\ License for more details.

\ You should have received a copy of the GNU General Public License
\ along with this program; if not, see <http://gnu.org/licenses>.

\ Fendo is written in Forth with Gforth
\ (<http://www.bernd-paysan.de/gforth.html>).

\ **************************************************************
\ Change history of this file

\ 2013-11-26: Start. First working version.
\ 2013-11-27: Change: several words renamed, after a new uniform
\   notation: "pid$" and "pid#" for both types of page ids.
\ 2014-03-02: Rewritten with 'traverse_pids'.
\ 2014-03-03: Draft pages are not included.
\ 2014-03-09: Improvement: faster, with '?exit' and rearranged
\ conditions.
\ 2014-03-10: Fix: double local.

\ **************************************************************
\ Requirements

\ From Fendo
require ./fendo.addon.traverse_pids.fs
require ./fendo.addon.regex.fs
require ./fendo.addon.dtddoc.fs

\ From Galope
require galope/module.fs  \ 'module:', ';module', 'hide', 'export'
require galope/rgx-wcmatch-question.fs  \ 'rgx-wcmatch?'

\ **************************************************************

module: fendo.addon.dloc_by_regex

: ((dloc_by_regex))  { D: pid -- }
  \ Create an element of a description list of content
  \ if the given pid matchs the current regex. 
  pid regex rgx-wcmatch? 0= ?exit
  pid pid$>data>pid# draft? ?exit
  pid dtddoc 
  ;
: (dloc_by_regex)  ( ca len -- f )
  \ ca len = pid
  \ f = continue with the next element?
  ((dloc_by_regex)) true
  ;

export

: dloc_by_regex  ( ca len -- )
  \ Create a description list of content
  \ with pages whose pid matchs the given regex.
  >regex  [<dl>] ['] (dloc_by_regex) traverse_pids [</dl>]
  ;

;module

.( fendo.addon.dloc_by_regex.fs compiled) cr
