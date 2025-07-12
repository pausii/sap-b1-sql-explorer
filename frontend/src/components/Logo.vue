<template>
  <div class="sap-logo-container">
    <div class="sap-logo"></div>
    <div class="sap-text">SAP</div>
  </div>
</template>

<style scoped>
/* Container styling */
.sap-logo-container {
  display: flex;
  align-items: center;
  gap: 15px;
  /* cursor: pointer; */
}

/* Logo animasi berputar */
.sap-logo {
  width: 70px;
  height: 70px;
  position: absolute;
  transform-style: preserve-3d;
  animation: rotate 8s ease-in-out infinite;
}

.sap-logo::before,
.sap-logo::after {
  content: '';
  position: absolute;
  top: 0;
  left: 0;
  width: 100%;
  height: 100%;
  border-radius: 50%;
  border: 3px solid transparent;
  mix-blend-mode: screen;
}

.sap-logo::before {
  border-top-color: #00a6e1;
  border-right-color: #00a6e1;
  animation: rotateColors1 4s ease-in-out infinite;
}

.sap-logo::after {
  border-bottom-color: #61bc46;
  border-left-color: #61bc46;
  animation: rotateColors2 4s ease-in-out infinite;
  animation-delay: 1s;
}

/* SAP Text */
.sap-text {
  font-size: 28px;
  font-weight: 700;
  position: relative;
  overflow: hidden;
  color: var(--sap-text-color);
}

.sap-text::before {
  content: 'SAP';
  position: absolute;
  top: 0;
  left: 0;
  width: 100%;
  height: 100%;
  animation: textGlow 2.5s ease-in-out infinite alternate, 
             textSlide 6s ease-in-out infinite;
  color: var(--sap-glow-color);
}

/* Light & Dark mode variable support */
@media (prefers-color-scheme: dark) {
  :root {
    --sap-text-color: #f3f4f6; /* light text for dark mode */
    --sap-glow-color: #00a6e1;
  }
}

@media (prefers-color-scheme: light) {
  :root {
    --sap-text-color: #1e293b; /* dark text for light mode */
    --sap-glow-color: #0079b8;
  }
}

/* Animasi putaran logo */
@keyframes rotate {
  0%, 100% { transform: rotateY(0) rotateX(10deg); }
  25% { transform: rotateY(90deg) rotateX(20deg); }
  50% { transform: rotateY(180deg) rotateX(10deg); }
  75% { transform: rotateY(270deg) rotateX(20deg); }
}

/* Perubahan warna animasi */
@keyframes rotateColors1 {
  0%, 100% {
    border-top-color: #00a6e1;
    border-right-color: #00a6e1;
  }
  50% {
    border-top-color: #61bc46;
    border-right-color: #61bc46;
  }
}

@keyframes rotateColors2 {
  0%, 100% {
    border-bottom-color: #61bc46;
    border-left-color: #61bc46;
  }
  50% {
    border-bottom-color: #00a6e1;
    border-left-color: #00a6e1;
  }
}

/* Efek glow dan clip */
@keyframes textGlow {
  0% {
    text-shadow: 0 0 5px rgba(0, 166, 225, 0.4);
  }
  100% {
    text-shadow:
      0 0 10px rgba(0, 166, 225, 0.8),
      0 0 20px rgba(0, 166, 225, 0.6);
  }
}

@keyframes textSlide {
  0%, 100% { clip-path: polygon(0 0, 100% 0, 100% 100%, 0 100%); }
  25% { clip-path: polygon(0 0, 60% 0, 40% 100%, 0 100%); }
  50% { clip-path: polygon(60% 0, 100% 0, 100% 100%, 40% 100%); }
  75% { clip-path: polygon(0 0, 100% 0, 100% 60%, 0 40%); }
  85% { clip-path: polygon(0 40%, 100% 60%, 100% 100%, 0 100%); }
}
</style>
