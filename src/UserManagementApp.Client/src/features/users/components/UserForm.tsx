import { useForm } from "react-hook-form";
import { zodResolver } from "@hookform/resolvers/zod";
import { z } from "zod";
import type { User, UserFormData } from "../types/user";
import { useEffect } from "react";

const schema = z.object({
  id: z.string().optional(),
  fullName: z.string().min(1, "Full Name is required"),
  email: z.string().email("Invalid email"),
  role: z.string().min(1, "Role is required"), // Changed to string to match your interface
  isActive: z.boolean(),
});
type UserFormSchema = z.infer<typeof schema>;

interface UserFormProps {
  initialData?: User | null;
  onSubmit: (data: UserFormData) => void;
  onCancel: () => void;
  isSubmitting: boolean;
}

export const UserForm = ({
  initialData,
  onSubmit,
  onCancel,
  isSubmitting,
}: UserFormProps) => {
  const {
    register,
    handleSubmit,
    reset,
    formState: { errors },
  } = useForm<UserFormSchema>({
    resolver: zodResolver(schema),
    defaultValues: { role: "User", isActive: true },
  });

  useEffect(() => {
    if (initialData) {
      reset({
        id: initialData.id,
        fullName: initialData.fullName,
        email: initialData.email,
        role: initialData.role as string,
        isActive: initialData.isActive,
      });
    }
  }, [initialData, reset]);

  return (
    <div className="fixed inset-0 bg-black bg-opacity-50 flex items-center justify-center p-4 z-50">
      <div className="bg-white rounded-lg shadow-xl w-full max-w-md p-6">
        <h2 className="text-xl font-bold mb-4">
          {initialData ? "Edit User" : "Add New User"}
        </h2>

        <form onSubmit={handleSubmit(onSubmit)} className="space-y-4">
          <div>
            <label className="block text-sm font-medium">Full Name</label>
            <input
              {...register("fullName")}
              className="w-full border rounded p-2 mt-1"
              placeholder="Enter your name"
            />
            {errors.fullName && (
              <p className="text-red-500 text-xs">{errors.fullName.message}</p>
            )}
          </div>

          <div>
            <label className="block text-sm font-medium">Email</label>
            <input
              {...register("email")}
              className="w-full border rounded p-2 mt-1"
              placeholder="Enter email"
            />
            {errors.email && (
              <p className="text-red-500 text-xs">{errors.email.message}</p>
            )}
          </div>

          <div>
            <label className="block text-sm font-medium">Role</label>
            <select
              {...register("role")}
              className="w-full border rounded p-2 mt-1"
            >
              <option value="User">User</option>
              <option value="Admin">Admin</option>
            </select>
          </div>

          <div className="flex justify-end gap-2 mt-6">
            <button
              type="button"
              onClick={onCancel}
              className="px-4 py-2 text-gray-600 bg-gray-100 rounded hover:bg-gray-200"
            >
              Cancel
            </button>
            <button
              type="submit"
              disabled={isSubmitting}
              className="px-4 py-2 bg-blue-600 rounded hover:bg-blue-700 disabled:opacity-50"
            >
              {isSubmitting ? "Saving..." : "Save"}
            </button>
          </div>
        </form>
      </div>
    </div>
  );
};
