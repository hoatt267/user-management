import { useQuery, useMutation, useQueryClient } from "@tanstack/react-query";
import toast from "react-hot-toast";
import { AxiosError } from "axios";
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
    onSuccess: () => {
      queryClient.invalidateQueries({ queryKey: ["users"] });
      toast.success("User created successfully!");
    },
    onError: (error: AxiosError<{ title?: string }>) => {
      const errorMessage =
        error?.response?.data?.title || "Failed to create user";
      toast.error(errorMessage);
    },
  });

  // Mutation: Update User
  const updateUserMutation = useMutation({
    mutationFn: ({ id, data }: { id: string; data: UserFormData }) =>
      userApi.update(id, data),
    onSuccess: () => {
      queryClient.invalidateQueries({ queryKey: ["users"] });
      toast.success("User updated successfully!");
    },
    onError: (error: AxiosError<{ title?: string }>) => {
      const errorMessage =
        error?.response?.data?.title || "Failed to update user";
      toast.error(errorMessage);
    },
  });

  // Mutation: Toggle Status
  const toggleStatusMutation = useMutation({
    mutationFn: ({ id, isActive }: { id: string; isActive: boolean }) =>
      userApi.toggleStatus(id, isActive),
    onSuccess: (_, variables) => {
      queryClient.invalidateQueries({ queryKey: ["users"] });
      const message = variables.isActive
        ? "User activated successfully!"
        : "User deactivated successfully!";
      toast.success(message);
    },
    onError: (error: AxiosError<{ title?: string }>) => {
      const errorMessage =
        error?.response?.data?.title || "Failed to toggle user status";
      toast.error(errorMessage);
    },
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
