<script lang="ts" setup>
import axios from 'axios';
import { onMounted } from 'vue';
import { useAppStore } from '../stores/app';

const app = useAppStore()

onMounted(() => {
    document.body.style.overflow = 'hidden';
    const baseUrl = import.meta.env.VITE_API_BASE_URL;
    axios.post(`${baseUrl}/auth/login`,{
        "username": import.meta.env.VITE_AUTH_USERNAME,
        "password": import.meta.env.VITE_AUTH_PASSWORD,
    }).then((res) => {
        setTimeout(() => {
            document.body.style.overflow = ''; // remove overflow
            app.isAuthenticated = true
            // app.setToken('Yoo')
            app.setToken(res.data.token)
        }, 3000)
    })
})
</script>

<template>
    <div>
        <div
            class="fixed inset-0 flex flex-col items-center justify-center z-50 bg-slate-100 dark:bg-slate-800 text-slate-100">
            <!-- Spinner -->
            <div class="spinner mb-4">
                <div></div>
                <div></div>
                <div></div>
                <div></div>
                <div></div>
                <div></div>
            </div>

            <!-- Teks di bawah spinner -->
            <p class="text-gray-600 text-lg mt-10">
                Authorizing
            </p>
        </div>
    </div>
</template>


<style scoped>
.spinner {
    width: 70.4px;
    height: 70.4px;
    animation: spinner-y0fdc1 2.8s infinite ease;
    transform-style: preserve-3d;
}


.spinner>div {
    background-color: rgba(156, 156, 156, 0.2);
    height: 100%;
    position: absolute;
    width: 100%;
    border: 3.5px solid #71767a;
}

[data-theme="dark"] {
    .spinner>div {
        background-color: rgba(63, 63, 77, 0.2);
        height: 100%;
        position: absolute;
        width: 100%;
        border: 3.5px solid #71767a;
    }
}

.spinner div:nth-of-type(1) {
    transform: translateZ(-35.2px) rotateY(180deg);
}

.spinner div:nth-of-type(2) {
    transform: rotateY(-270deg) translateX(50%);
    transform-origin: top right;
}

.spinner div:nth-of-type(3) {
    transform: rotateY(270deg) translateX(-50%);
    transform-origin: center left;
}

.spinner div:nth-of-type(4) {
    transform: rotateX(90deg) translateY(-50%);
    transform-origin: top center;
}

.spinner div:nth-of-type(5) {
    transform: rotateX(-90deg) translateY(50%);
    transform-origin: bottom center;
}

.spinner div:nth-of-type(6) {
    transform: translateZ(35.2px);
}

@keyframes spinner-y0fdc1 {
    0% {
        transform: rotate(45deg) rotateX(-25deg) rotateY(25deg);
    }

    50% {
        transform: rotate(45deg) rotateX(-385deg) rotateY(25deg);
    }

    100% {
        transform: rotate(45deg) rotateX(-385deg) rotateY(385deg);
    }
}
</style>