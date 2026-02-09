import { useState } from "react";
import { useUsers } from "../hooks/useUser";
import { StatusBadge } from "./StatusBadge";
import { UserForm } from "./UserForm";
import type { User, UserFormData } from "../types/user";
import type { Column } from "../../../components/Table/type";
import { DataTable } from "../../../components/Table/DataTable";
import { useDebounce } from "../../../hooks/useDebounce";

export const UserTable = () => {
  const [page, setPage] = useState(1);
  const [pageSize, setPageSize] = useState(5);
  const [search, setSearch] = useState("");
  const debouncedSearch = useDebounce(search, 500);

  const {
    users: paginatedData,
    isLoading,
    createUser,
    updateUser,
    toggleStatus,
    isCreating,
    isUpdating,
  } = useUsers({
    pageNumber: page,
    pageSize,
    searchKey: debouncedSearch,
  });

  const [editingUser, setEditingUser] = useState<User | null>(null);
  const [isFormOpen, setIsFormOpen] = useState(false);

  const handleSave = async (data: UserFormData) => {
    try {
      if (editingUser) await updateUser({ id: editingUser.id, data });
      else await createUser(data);

      setIsFormOpen(false);
      setEditingUser(null);
    } catch {
      // Error is already handled by the mutation's onError callback
    }
  };

  const columns: Column<User>[] = [
    {
      header: "Name",
      accessor: "fullName",
      sortable: true,
    },
    {
      header: "Email",
      accessor: "email",
      className: "text-gray-500",
      sortable: true,
    },
    {
      header: "Role",
      accessor: "role",
    },
    {
      header: "Status",
      render: (user) => <StatusBadge isActive={user.isActive} />,
    },
    {
      header: "Actions",
      render: (user) => (
        <div className="text-right space-x-3">
          <button
            onClick={() =>
              toggleStatus({ id: user.id, isActive: !user.isActive })
            }
            className="text-sm underline text-gray-600 hover:text-black"
          >
            {user.isActive ? "Deactivate" : "Activate"}
          </button>

          <button
            onClick={() => {
              setEditingUser(user);
              setIsFormOpen(true);
            }}
            className="text-sm font-medium text-blue-600 hover:text-blue-800"
          >
            Edit
          </button>
        </div>
      ),
      className: "text-right",
    },
  ];

  return (
    <div className="p-6 max-w-6xl mx-auto">
      {/* Header */}
      <div className="flex justify-between items-center mb-6">
        <h1 className="text-2xl font-bold text-pink-300">
          User Management Test
        </h1>
      </div>
      <div>
        <button
          onClick={() => {
            setEditingUser(null);
            setIsFormOpen(true);
          }}
          className="bg-blue-600 px-4 py-2 rounded shadow hover:bg-blue-700"
        >
          + Add User
        </button>
      </div>
      {/* Search */}
      <input
        type="text"
        placeholder="Search..."
        className="mb-4 border p-2 rounded w-64"
        value={search}
        onChange={(e) => {
          setSearch(e.target.value);
          setPage(1);
        }}
      />

      {/* DataTable */}
      <DataTable<User>
        data={paginatedData?.items ?? []}
        columns={columns}
        loading={isLoading}
        currentPage={paginatedData?.pageNumber ?? 1}
        totalPages={paginatedData?.totalPages ?? 1}
        pageSize={pageSize}
        totalCount={paginatedData?.totalCount ?? 0}
        onPageChange={(p) => setPage(p)}
        onPageSizeChange={(size) => {
          setPageSize(size);
          setPage(1);
        }}
      />

      {/* Form Modal */}
      {isFormOpen && (
        <UserForm
          initialData={editingUser}
          onSubmit={handleSave}
          onCancel={() => setIsFormOpen(false)}
          isSubmitting={isCreating || isUpdating}
        />
      )}
    </div>
  );
};
