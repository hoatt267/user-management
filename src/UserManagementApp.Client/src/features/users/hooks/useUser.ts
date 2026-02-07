import { useQuery, useMutation, useQueryClient } from "@tanstack/react-query";
import type { UserFormData } from "../types/user";
import type { PaginatedFilter } from "../../../types/paginated-filter";
import { userApi } from "../services/userApi";

export const useUsers = (filter?: PaginatedFilter) => {
  const queryClient = useQueryClient();

  // Query: Fetch Users with Pagination
  const usersQuery = useQuery({
    queryKey: ["users", filter],
    queryFn: () =>
      userApi.getAll(filter || { pageNumber: 1, pageSize: 10, searchKey: "" }),
  });

  // Query: Get User by ID
  const useUserById = (id: string) =>
    useQuery({
      queryKey: ["users", id],
      queryFn: () => userApi.getUserById(id),
      enabled: !!id,
    });

  // Mutation: Create User
  const createUserMutation = useMutation({
    mutationFn: userApi.create,
    onSuccess: () => queryClient.invalidateQueries({ queryKey: ["users"] }),
  });

  // Mutation: Update User
  const updateUserMutation = useMutation({
    mutationFn: ({ id, data }: { id: string; data: UserFormData }) =>
      userApi.update(id, data),
    onSuccess: () => queryClient.invalidateQueries({ queryKey: ["users"] }),
  });

  // Mutation: Toggle Status
  const toggleStatusMutation = useMutation({
    mutationFn: ({ id, isActive }: { id: string; isActive: boolean }) =>
      userApi.toggleStatus(id, isActive),
    onSuccess: () => queryClient.invalidateQueries({ queryKey: ["users"] }),
  });

  return {
    // Queries
    users: usersQuery.data,
    isLoading: usersQuery.isLoading,
    isError: usersQuery.isError,
    error: usersQuery.error,
    useUserById: useUserById,

    // Mutations
    createUser: createUserMutation.mutateAsync,
    updateUser: updateUserMutation.mutateAsync,
    toggleStatus: toggleStatusMutation.mutateAsync,

    // Loading states
    isCreating: createUserMutation.isPending,
    isUpdating: updateUserMutation.isPending,
    isTogglingStatus: toggleStatusMutation.isPending,
  };
};
