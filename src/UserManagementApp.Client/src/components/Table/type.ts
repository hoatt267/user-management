export interface Column<T> {
  header: string;
  accessor?: keyof T;
  render?: (row: T) => React.ReactNode;
  sortable?: boolean;
  className?: string;
}

export interface DataTableProps<T> {
  data: T[];
  columns: Column<T>[];
  loading?: boolean;

  // Pagination
  currentPage: number;
  totalPages: number;
  pageSize: number;
  totalCount: number;

  onPageChange: (page: number) => void;
  onPageSizeChange?: (size: number) => void;

  // Sorting
  sortKey?: string;
  sortDirection?: "asc" | "desc";
  onSort?: (key: string) => void;
}
