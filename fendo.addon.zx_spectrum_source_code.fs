.( fendo.addon.zx_spectrum_source_code.fs) cr

\ This file is part of Fendo.

\ This file is the ZX Spectrum source code addon.

\ Copyright (C) 2014 Marcos Cruz (programandala.net)

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

\ See at the end of the file.

\ **************************************************************
\ Requirements

fendo_definitions

require ./fendo.addon.source_code.fs
require ./fendo.addon.zx_spectrum_charset.fs

\ **************************************************************

: zx_spectrum_source_code  ( ca len -- )
  \ Read and echo the content of a ZX Spectrum source code file.
  \ The Vim filetype is guessed from the filename, unless
  \ already set in the 'programming_language$' dynamic string.
  \ ca len = file name
  ['] zx_spectrum_source_code_translated is source_code_posttranslated
  source_code
  ;

\ **************************************************************
\ Change history of this file

\ 2014-10-17: Start, with part of the file <fendo.addon.source_code.fs>.

.( fendo.addon.zx_spectrum_source_code.fs compiled) cr
