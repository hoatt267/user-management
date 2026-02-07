import { Pagination } from "./Pagination";
import { pageSizeOptions } from "./helpers";
import type { DataTableProps } from "./type";

export function DataTable<T>({
  data,
  columns,
  loading,
  currentPage,
  totalPages,
  pageSize,
  totalCount,
  onPageChange,
  onPageSizeChange,
  sortKey,
  sortDirection,
  onSort,
}: DataTableProps<T>) {
  return (
    <div className="bg-white shadow-lg rounded-2xl overflow-hidden">
      <div className="overflow-x-auto">
        <table className="min-w-full text-sm">
          <thead className="bg-gray-100 text-xs uppercase">
            <tr>
              {columns.map((col, index) => (
                <th
                  key={index}
                  className={`px-6 py-3 text-left ${
                    col.sortable ? "cursor-pointer select-none" : ""
                  }`}
                  onClick={() =>
                    col.sortable &&
                    col.accessor &&
                    onSort?.(String(col.accessor))
                  }
                >
                  <div className="flex items-center gap-1">
                    {col.header}
                    {sortKey === col.accessor && (
                      <span>{sortDirection === "asc" ? "▲" : "▼"}</span>
                    )}
                  </div>
                </th>
              ))}
            </tr>
          </thead>

          <tbody className="divide-y">
            {loading ? (
              <tr>
                <td colSpan={columns.length} className="text-center py-6">
                  Loading...
                </td>
              </tr>
            ) : data.length === 0 ? (
              <tr>
                <td colSpan={columns.length} className="text-center py-6">
                  No data found
                </td>
              </tr>
            ) : (
              data.map((row, rowIndex) => (
                <tr key={rowIndex} className="hover:bg-gray-50">
                  {columns.map((col, colIndex) => (
                    <td
                      key={colIndex}
                      className={`px-6 py-4 ${col.className ?? ""}`}
                    >
                      {col.render
                        ? col.render(row)
                        : col.accessor
                          ? String(row[col.accessor] ?? "")
                          : null}
                    </td>
                  ))}
                </tr>
              ))
            )}
          </tbody>
        </table>
      </div>

      {/* Footer */}
      <div className="flex items-center justify-between px-6 py-4 bg-gray-50">
        <div className="text-sm text-gray-600">
          Showing {(currentPage - 1) * pageSize + 1} -
          {Math.min(currentPage * pageSize, totalCount)} of {totalCount}
        </div>

        <div className="flex items-center gap-4">
          {onPageSizeChange && (
            <select
              value={pageSize}
              onChange={(e) => onPageSizeChange(Number(e.target.value))}
              className="border rounded-md px-2 py-1 text-sm"
            >
              {pageSizeOptions.map((size) => (
                <option key={size} value={size}>
                  {size} / page
                </option>
              ))}
            </select>
          )}

          <Pagination
            currentPage={currentPage}
            totalPages={totalPages}
            onPageChange={onPageChange}
          />
        </div>
      </div>
    </div>
  );
}
