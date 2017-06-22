.( fendo.addon.asciidoctor.fs) cr

\ This file is part of Fendo
\ (http://programandala.net/en.program.fendo.html).

\ This file is the Asciidoctor addon. It provides words to include
\ contents in Asciidoctor (or AsciiDoc) format, either inline or from
\ a file.

\ Last modified 20170622.
\ See change log at the end of the file.

\ Copyright (C) 2015,2017 Marcos Cruz (programandala.net)

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

\ Fendo is written in Forth (http://forth-standard.org)
\ with Gforth (<http://gnu.org/software/gforth>).

\ ==============================================================
\ Requirements

\ Asciidoctor must be installed in the system.  See:
\ <http://asciidoctor.org>.
\
\ The original AsciiDoc engine could be used instead, but this file
\ should be adapted.  Support for AsciidDoc is planned.  See:
\ <http://asciidoc.org>.

forth_definitions

require galope/module.fs  \ 'module:', ';module', 'hide', 'export'

fendo_definitions

\ ==============================================================

module: fendo.addon.asciidoctor

hide

s" /tmp/fendo_addon.asciidoctor.adoc" 2dup 2constant input_file$
s" .html" s+ 2constant output_file$

\ XXX TODO -- There are similar words '>input_file' and '<output_file'
\ in <fendo.addon.source_code.common.fs>; maybe they can be shared.

: >input_file ( ca len -- )
  input_file$ w/o create-file throw
  dup >r write-file throw  r> close-file throw ;
  \ Save the given contents to the file that Asciidoctor will load as input.
  \ ca len = text in Asciidoctor format

: <output_file ( -- ca len )
  output_file$ slurp-file ;
  \ Get the content of the file that Asciidoctor created as output.
  \ ca len = contents in HTML format

\ XXX TODO Use also AsciiDoc as alternative. Maybe 'asciidoc_command'
\ and a defered 'adoc_command'.

\ The Asciidoctor command includes all the required options, even when
\ they are the default; just in case the deafaults change in the
\ future, and also for the sake of clarity:
s" asciidoctor "
s" --backend html5 " s+     \ default
s" --doctype article " s+   \ default
s" --no-header-footer " s+  \ supress the document header and footer
                            \ in the output
s" --compact " s+           \ remove blank lines in the output
s" --out-file " s+ output_file$ s+
2constant asciidoctor_base_command$

: asciidoctor_command$ ( -- ca len )
  asciidoctor_base_command$ s"  " s+ ;
  \ Return the Asciidoctor command without the input file.

: (include_asciidoctor) ( ca1 len1 -- ca2 len2 )
  file>local asciidoctor_command$ s"  " s+ 2swap s+
  system $? abort" The system asciidoctor command failed"
  <output_file ;
  \ Return the contents _ca2 len2_ converted
  \ from the Asciidoctor file _ca1 len1_.
  \ The header and footer of the file will be ignored.

export

: include_asciidoctor ( ca1 len1 -- )
  (include_asciidoctor) echo ;
  \ Include contents in Asciidoctor format from the given file.
  \ The header and footer of the file will be ignored.

0 [if]

: asciidoctor{ ( "ccc<}asciidoctor>" -- )
  ;
  \ Start interpreting contents in Asciidoctor format,
  \ until "}asciidoctor" is found.
  \ XXX TODO -- as a macro

[then]

;module

.( fendo.addon.asciidoctor.fs compiled) cr

\ ==============================================================
\ Change log

\ 2015-02-11: Start. 'include_asciidoctor' works.
\ 2015-09-03: Added 'file>local' to 'include_asciidoctor'.
\ 2017-02-10: Factor `(include_asciidoctor)` from
\ `include_asciidoctor`, to reuse it for README.adoc files, which need
\ additional operations.
\ 2017-06-22: Update source style, layout and header.

\ vim: filetype=gforth
