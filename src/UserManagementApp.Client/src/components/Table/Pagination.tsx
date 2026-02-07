import { getPaginationRange } from "./helpers";

interface Props {
  currentPage: number;
  totalPages: number;
  onPageChange: (page: number) => void;
}

export function Pagination({ currentPage, totalPages, onPageChange }: Props) {
  const pages = getPaginationRange(currentPage, totalPages);

  return (
    <div className="flex items-center gap-2">
      {pages.map((page, i) =>
        page === "..." ? (
          <span key={i} className="px-2 text-gray-400">
            ...
          </span>
        ) : (
          <button
            key={i}
            onClick={() => onPageChange(page)}
            className={`px-3 py-1 rounded-md text-sm border ${
              page === currentPage ? "bg-blue-600" : "hover:bg-gray-100"
            }`}
          >
            {page}
          </button>
        ),
      )}
    </div>
  );
}
