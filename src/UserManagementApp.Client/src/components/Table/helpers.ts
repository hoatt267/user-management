export function getPaginationRange(
  currentPage: number,
  totalPages: number,
): (number | "...")[] {
  const delta = 1;
  const range: (number | "...")[] = [];

  for (let i = 1; i <= totalPages; i++) {
    if (
      i === 1 ||
      i === totalPages ||
      (i >= currentPage - delta && i <= currentPage + delta)
    ) {
      range.push(i);
    } else if (i === currentPage - delta - 1 || i === currentPage + delta + 1) {
      range.push("...");
    }
  }

  return range;
}

export const pageSizeOptions = [5, 10, 20, 50];
