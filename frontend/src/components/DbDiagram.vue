<template>
  <svg ref="svgRef" width="100%" height="600"></svg>
</template>

<script setup>
import { onMounted, ref, nextTick } from 'vue'
import * as d3 from 'd3'

const svgRef = ref()

// Dummy data
const tables = [
  {
    id: "tableA",
    name: "Users",
    x: 100,
    y: 100,
    columns: ["id", "name", "email"]
  },
  {
    id: "tableB",
    name: "Addresses",
    x: 400,
    y: 200,
    columns: ["id", "user_id", "address"]
  }
];


const links = [
  { source: { table: "tableA", column: "id" }, target: { table: "tableB", column: "user_id" } }
]

onMounted(async () => {
  await nextTick()
  const svg = d3.select(svgRef.value)

  // Buat grup tabel
  const tableGroups = svg.selectAll("g.table-group")
    .data(tables)
    .enter()
    .append("g")
    .attr("class", "table-group")
    .attr("transform", d => `translate(${d.x},${d.y})`)
    .call(d3.drag()
      .on("drag", function (event, d) {
        d.x += event.dx
        d.y += event.dy
        d3.select(this).attr("transform", `translate(${d.x},${d.y})`)
        drawLinks()
      })
    )

  // Gambar kotak tabel
  tableGroups.append("rect")
    .attr("class", "table")
    .attr("width", 150)
    .attr("height", d => d.columns.length * 30 + 10)
    .attr("rx", 10)
    .attr("fill", "#fff")
    .attr("stroke", "none")
    .attr("stroke-width", 2)

  // Gambar kolom per tabel
  tableGroups.each(function (d) {
  const group = d3.select(this)

  // Header background (opsional, untuk gaya)
  group.append("rect")
    .attr("x", 0)
    .attr("y", 0)
    .attr("width", 150)
    .attr("height", 30)
    .attr("fill", "#333")

  // Header teks (nama tabel)
  group.append("text")
    .attr("x", 75) // tengah (150 / 2)
    .attr("y", 20) // di tengah header
    .attr("text-anchor", "middle")
    .attr("font-size", "14px")
    .attr("font-weight", "bold")
    .attr("fill", "#fff")
    .text(d.name)

  // Kolom-kolom tabel
  d.columns.forEach((col, i) => {
    const yOffset = 30 + i * 30 // dimulai setelah header (30px tinggi)

    group.append("rect")
      .attr("x", 10)
      .attr("y", yOffset)
      .attr("width", 130)
      .attr("height", 25)
      .attr("fill", "#e0e0e0")
      .attr("stroke", "#999")
      .attr("id", `${d.id}_${col}`)

    group.append("text")
      .attr("x", 15)
      .attr("y", yOffset + 17) // teks sejajar di tengah kolom (offset 17 dari atas kolom)
      .attr("font-size", "12px")
      .attr("fill", "#000")
      .text(col)
  })
})


  function getColumnCenter(tableId, column, side = 'center') {
    const svgRect = svgRef.value.getBoundingClientRect();
    const node = d3.select(`#${tableId}_${column}`).node();
    const bbox = node.getBoundingClientRect();

    let x = bbox.left - svgRect.left;
    if (side === 'right') x += bbox.width;
    if (side === 'center') x += bbox.width / 2;

    return {
      x,
      y: bbox.top - svgRect.top + bbox.height / 2
    };
  }


  function drawLinks() {
    svg.selectAll("path.link").remove()

    links.forEach(link => {
      const source = getColumnCenter(link.source.table, link.source.column, 'right')
      const target = getColumnCenter(link.target.table, link.target.column, 'left')


      const path = d3.path()
      path.moveTo(source.x, source.y)
      path.lineTo((source.x + target.x) / 2, source.y)
      path.lineTo((source.x + target.x) / 2, target.y)
      path.lineTo(target.x, target.y)

      svg.append("path")
        .attr("class", "link")
        .attr("d", path.toString())
        .attr("stroke", "#3498db")
        .attr("stroke-width", 2)
        .attr("fill", "none")
    })
  }

  drawLinks()
})
</script>