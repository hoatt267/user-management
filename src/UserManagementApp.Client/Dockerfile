# === STAGE 1: BUILD ===
FROM node:18-alpine AS build
WORKDIR /app

# Build arguments for environment variables
ARG VITE_API_BASE_URL=http://localhost:8080/api
ARG VITE_API_TIMEOUT=30000

# Set environment variables for build
ENV VITE_API_BASE_URL=$VITE_API_BASE_URL
ENV VITE_API_TIMEOUT=$VITE_API_TIMEOUT

# Copy package.json first (Caching!)
COPY src/UserManagementApp.Client/package*.json ./
RUN npm install

# Copy source code and Build
COPY src/UserManagementApp.Client/ .
RUN npm run build

# === STAGE 2: SERVE ===
FROM nginx:alpine

# Copy the build output (dist folder) to Nginx's html folder
COPY --from=build /app/dist /usr/share/nginx/html

# Custom Nginx config (Optional but recommended for React Router)
# This fixes the "404 Not Found" error when you refresh the page on a sub-route
COPY src/UserManagementApp.Client/nginx.conf /etc/nginx/conf.d/default.conf

EXPOSE 80
CMD ["nginx", "-g", "daemon off;"]