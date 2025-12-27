#!/usr/bin/env bash

OUTPUT="csv_inspection.txt"

echo "CSV Inspection Report" > "$OUTPUT"
echo "Generated on: $(date)" >> "$OUTPUT"
echo "======================================" >> "$OUTPUT"
echo >> "$OUTPUT"

for f in *.csv; do
  echo "===== $f =====" >> "$OUTPUT"

  echo -n "Rows: " >> "$OUTPUT"
  xsv count "$f" >> "$OUTPUT"

  echo "--- HEAD (5 rows) ---" >> "$OUTPUT"
  xsv slice -s 0 -l 5 "$f" | xsv table >> "$OUTPUT"

  echo >> "$OUTPUT"
done

echo "Done. Output written to $OUTPUT"
