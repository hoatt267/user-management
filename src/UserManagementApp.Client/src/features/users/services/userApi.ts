import axiosClient from "../../../api/axiosClient";
import type { PaginatedFilter } from "../../../types/paginated-filter";
import type { PaginatedList } from "../../../types/paginated-list";
import type { User, UserFormData } from "../types/user";
import type { GeneralResponse } from "../../../types/general-response"; // Import the new type

export const userApi = {
  getAll: async (userFilter: PaginatedFilter) => {
    const response = await axiosClient.get<
      GeneralResponse<PaginatedList<User>>
    >("/users", {
      params: userFilter,
    });
    return response.data.data;
  },

  getUserById: async (id: string) => {
    const response = await axiosClient.get<GeneralResponse<User>>(
      `/users/${id}`,
    );
    return response.data.data;
  },

  create: async (data: UserFormData) => {
    const response = await axiosClient.post<GeneralResponse<string>>(
      "/users",
      data,
    );
    return response.data.data;
  },

  update: async (id: string, data: UserFormData) => {
    const response = await axiosClient.put<GeneralResponse<string>>(
      `/users/${id}`,
      data,
    );
    return response.data.data;
  },

  toggleStatus: async (id: string, isActive: boolean) => {
    const response = await axiosClient.patch<GeneralResponse<string>>(
      `/users/${id}/status`,
      { id, isActive },
    );
    return response.data.data;
  },
};
