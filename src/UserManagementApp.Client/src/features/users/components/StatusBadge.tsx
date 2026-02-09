export const StatusBadge = ({ isActive }: { isActive: boolean }) => {
  return (
    <span
      className={`px-2 py-1 rounded-full text-xs font-semibold border ${
        isActive
          ? "bg-green-100 text-green-800 border-green-200"
          : "bg-red-100 text-red-800 border-red-200"
      }`}
    >
      {isActive ? "Active" : "Inactive"}
    </span>
  );
};
